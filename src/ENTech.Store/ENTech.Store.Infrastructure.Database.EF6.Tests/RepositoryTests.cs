using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class RepositoryTests
	{
		public class For_IEntity
		{
			private readonly Repository<StubDomainEntity, StubDbEntity> _repository;
			private readonly ObservableCollection<StubDbEntity> _dbSetData;

			private Mock<IDbSet<StubDbEntity>> _dbSetMock = new Mock<IDbSet<StubDbEntity>>();
			private Mock<IDbEntityStateKeeper<StubDomainEntity, StubDbEntity>> _entityStateManagerMock = new Mock<IDbEntityStateKeeper<StubDomainEntity, StubDbEntity>>();
			private Mock<IDbEntityMapper> _dbEntityMapperMock = new Mock<IDbEntityMapper>();

			private readonly StubDbEntity _firstEntity = new StubDbEntity { Id = 1, PropertyText = "Text Text 1" };
			private readonly StubDbEntity _secondEntity = new StubDbEntity { Id = 2, PropertyText = "Text Text 2" };
			private readonly StubDbEntity _thirdEntity = new StubDbEntity { Id = 3, PropertyText = "Text Text 3" };
			private readonly StubDbEntity _fourthEntityWithDupeId = new StubDbEntity { Id = 4, PropertyText = "Text Text 4" };
			private readonly StubDbEntity _fifthEntityWithDupeId = new StubDbEntity { Id = 4, PropertyText = "Text Text 5" };

			[TearDown]
			public void TearDown()
			{
				_dbSetMock.ResetCalls();
				_dbEntityMapperMock.ResetCalls();
				_entityStateManagerMock.ResetCalls();
			}

			public For_IEntity()
			{
				_dbSetData = new ObservableCollection<StubDbEntity>
				{
					_firstEntity,
					_secondEntity,
					_thirdEntity,
					_fourthEntityWithDupeId,
					_fifthEntityWithDupeId
				};

				var dataQueryable = _dbSetData.AsQueryable();

				_dbSetMock.Setup(x => x.Local).Returns(_dbSetData);

				_dbSetMock.Setup(x => x.Expression).Returns(dataQueryable.Expression);
				_dbSetMock.Setup(x => x.ElementType).Returns(dataQueryable.ElementType);
				_dbSetMock.Setup(x => x.Provider).Returns(dataQueryable.Provider);

				_dbEntityMapperMock.Setup(x => x.MapToEntity<StubDbEntity, StubDomainEntity>(It.IsAny<StubDbEntity>()))
					.Returns((StubDbEntity dbEntity) => new StubDomainEntity
					{
						Id = dbEntity.Id,
						Text = dbEntity.PropertyText
					});

				_dbEntityMapperMock.Setup(x => x.MapToEntities<StubDbEntity, StubDomainEntity>(It.IsAny<IEnumerable<StubDbEntity>>()))
					.Returns((IEnumerable<StubDbEntity> dbEntities) => dbEntities.Select(dbEntity => new StubDomainEntity
					{
						Id = dbEntity.Id,
						Text = dbEntity.PropertyText
					}));

				_repository = new Repository<StubDomainEntity, StubDbEntity>(_dbSetMock.Object, _entityStateManagerMock.Object, _dbEntityMapperMock.Object);
			}

			#region GetById
			[Test]
			public void GetById_When_called_Then_gets_item_with_correct_id_from_collecion()
			{
				var entityId = _thirdEntity.Id;

				var entity = new StubDomainEntity{Id = _thirdEntity.Id};

				_dbEntityMapperMock.Setup(x => x.MapToEntity<StubDbEntity, StubDomainEntity>(_thirdEntity)).Returns(entity);
				
				var result = _repository.GetById(entityId);

				Assert.AreEqual(entity, result);
			}

			[Test]
			public void GetById_When_called_for_entity_with_dupe_id_Then_does_not_throw()
			{
				var entityId = _fourthEntityWithDupeId.Id;

				Assert.DoesNotThrow(() => _repository.GetById(entityId));
			}

			[Test]
			public void GetById_When_called_for_entity_Then_stores_that_entity_in_entityStateManager()
			{
				var entityId = _thirdEntity.Id;
				var entity = new StubDomainEntity();

				_dbEntityMapperMock.Setup(x => x.MapToEntity<StubDbEntity, StubDomainEntity>(_thirdEntity)).Returns(entity);

				_repository.GetById(entityId);

				_entityStateManagerMock.Verify(x => x.Store(entity, _thirdEntity));
			}

			[Test]
			public void GetById_When_called_for_entity_Then_maps_dbEntity_to_Entity()
			{
				var entityId = _thirdEntity.Id;

				_repository.GetById(entityId);

				_dbEntityMapperMock.Verify(x => x.MapToEntity<StubDbEntity, StubDomainEntity>(_thirdEntity), Times.Once);
			}
			#endregion

			#region FindByIds
			[Test]
			public void FindByIds_When_called_with_correct_ids_Then_stores_these_entities_in_entityStateManager()
			{
				var entityIds = new[] { _firstEntity.Id, _secondEntity.Id, _thirdEntity.Id };

				var entities = new[] { new StubDomainEntity { Id = _firstEntity.Id }, new StubDomainEntity { Id = _secondEntity.Id }, new StubDomainEntity { Id = _thirdEntity.Id } };

				_dbEntityMapperMock.Setup(
					x =>
						x.MapToEntities<StubDbEntity, StubDomainEntity>(
							It.Is<IEnumerable<StubDbEntity>>(
								y => y.Contains(_firstEntity) && y.Contains(_secondEntity) && y.Contains(_thirdEntity)))).Returns(entities);

				_repository.FindByIds(entityIds);

				_entityStateManagerMock.Verify(x => x.Store(entities.ElementAt(0), _firstEntity), Times.Once);
				_entityStateManagerMock.Verify(x => x.Store(entities.ElementAt(1), _secondEntity), Times.Once);
				_entityStateManagerMock.Verify(x => x.Store(entities.ElementAt(2), _thirdEntity), Times.Once);
			}

			[Test]
			public void FindByIds_When_called_with_correct_ids_Then_returns_correct_collection_with_items_with_these_ids()
			{
				var entityIds = new[] { _firstEntity.Id, _secondEntity.Id, _thirdEntity.Id };

				var results = _repository.FindByIds(entityIds);

				var resultIds = results.Select(x => x.Id).ToList();

				Assert.Contains(_firstEntity.Id, resultIds);
				Assert.Contains(_secondEntity.Id, resultIds);
				Assert.Contains(_thirdEntity.Id, resultIds);
			}

			[Test]
			public void FindByIds_When_called_with_ids_some_of_which_can_not_be_found_Then_throws_exception()
			{
				var entityIds = new[] { _firstEntity.Id, _secondEntity.Id, -1 };

				Assert.Throws<EntityLoadMismatchException>(() => _repository.FindByIds(entityIds));
			}
			#endregion

			#region Add
			[Test]
			public void Add_When_called_for_entity_Then_calls_dbEntityMapper_create()
			{
				var stubEntity = new StubDomainEntity();

				_repository.Add(stubEntity);

				_dbEntityMapperMock.Verify(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(stubEntity), Times.Once);
			}

			[Test]
			public void Add_When_called_for_entity_Then_calls_dbSet_add_for_entity_from_dbEntityMapper_createDbEntity()
			{
				var stubEntity = new StubDomainEntity();

				var stubDbEntity = new StubDbEntity();

				_dbEntityMapperMock.Setup(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(stubEntity))
					.Returns(stubDbEntity);


				_repository.Add(stubEntity);

				_dbSetMock.Verify(x => x.Add(stubDbEntity));
			}

			[Test]
			public void Add_When_called_for_multiple_entities_Then_calls_dbEntityMapper_create_for_each_entity()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity { Text = "hello"},
					new StubDomainEntity { Text = "world"},
					new StubDomainEntity { Text = "another"}
				};

				_repository.Add(entityCollection);

				for (var i = 0; i < entityCollection.Count; i++)
				{
					_dbEntityMapperMock.Verify(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(entityCollection.ElementAt(i)), Times.Once);
				}
			}

			[Test]
			public void Add_When_called_for_multiple_entities_Then_calls_dbSet_add_for_each_entity()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity { Text = "hello"},
					new StubDomainEntity { Text = "world"},
					new StubDomainEntity { Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity { PropertyText = "hello"},
					new StubDbEntity { PropertyText = "world"},
					new StubDbEntity { PropertyText = "another"}
				};

				_dbEntityMapperMock.Setup(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(entityCollection.ElementAt(0)))
					.Returns(dbEntityCollection.ElementAt(0));
				
				_dbEntityMapperMock.Setup(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(entityCollection.ElementAt(1)))
					.Returns(dbEntityCollection.ElementAt(1));
				
				_dbEntityMapperMock.Setup(x => x.CreateDbEntity<StubDomainEntity, StubDbEntity>(entityCollection.ElementAt(2)))
					.Returns(dbEntityCollection.ElementAt(2));

				

				_repository.Add(entityCollection);

				for (var i = 0; i < entityCollection.Count; i++)
				{
					_dbSetMock.Verify(x => x.Add(dbEntityCollection.ElementAt(i)));
				}
			}
			#endregion

			#region Delete
			[Test]
			public void Delete_When_called_for_entity_without_logically_deletable_interface_Then_calls_entityStateManager_get()
			{
				var stubEntity = new StubDomainEntity();
				var stubDbEntity = new StubDbEntity();
				_entityStateManagerMock.Setup(x => x.Get(stubEntity)).Returns(stubDbEntity);

				_repository.Delete(stubEntity);

				_entityStateManagerMock.Verify(x => x.Get(stubEntity), Times.Once);
			}
			
			[Test]
			public void Delete_When_called_for_entity_without_logically_deletable_interface_Then_calls_entityStateManager_clear_after_deletion()
			{
				var stubEntity = new StubDomainEntity();
				var stubDbEntity = new StubDbEntity();
				_entityStateManagerMock.Setup(x => x.Get(stubEntity)).Returns(stubDbEntity);

				_repository.Delete(stubEntity);

				_entityStateManagerMock.Verify(x => x.Remove(stubEntity), Times.Once);
			}

			[Test]
			public void Delete_When_called_for_entity_that_is_not_in_entityStateManager_Then_throws_entityPersistenceException()
			{
				var stubEntity = new StubDomainEntity();

				Assert.Throws<EntityPersistenceException>(() => _repository.Delete(stubEntity));
			}

			[Test]
			public void Delete_When_called_for_entity_without_logically_deletable_interface_Then_calls_dbSet_remove()
			{
				var stubEntity = new StubDomainEntity();
				var stubDbEntity = new StubDbEntity();
				_entityStateManagerMock.Setup(x => x.Get(stubEntity)).Returns(stubDbEntity);
				
				_repository.Delete(stubEntity);

				_dbSetMock.Verify(x => x.Remove(stubDbEntity));
			}

			[Test]
			public void Delete_When_called_for_multiple_entities_without_logically_deletable_interface_Then_calls_dbSet_remove_for_each_entity()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity {Id = 1, Text = "hello"},
					new StubDomainEntity {Id = 2, Text = "world"},
					new StubDomainEntity {Id = 3, Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity {Id = 1, PropertyText = "hello"},
					new StubDbEntity {Id = 2, PropertyText = "world"},
					new StubDbEntity {Id = 3, PropertyText = "another"}
				};

				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(0))).Returns(dbEntityCollection.ElementAt(0));
				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(2))).Returns(dbEntityCollection.ElementAt(2));

				Assert.Throws<EntityPersistenceException>(() => _repository.Delete(entityCollection));
			}

			[Test]
			public void Delete_When_called_for_multiple_entities_without_logically_deletable_interface_and_at_least_one_entity_is_not_attached_Then_throws_entityPersistenceException()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity {Id = 1, Text = "hello"},
					new StubDomainEntity {Id = 2, Text = "world"},
					new StubDomainEntity {Id = 3, Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity {Id = 1, PropertyText = "hello"},
					new StubDbEntity {Id = 2, PropertyText = "world"},
					new StubDbEntity {Id = 3, PropertyText = "another"}
				};

				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(0))).Returns(dbEntityCollection.ElementAt(0));
				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(1))).Returns(dbEntityCollection.ElementAt(1));
				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(2))).Returns(dbEntityCollection.ElementAt(2));


				_repository.Delete(entityCollection);


				foreach (var stubEntity in dbEntityCollection)
				{
					var entity = stubEntity;
					_dbSetMock.Verify(x => x.Remove(entity));
				}
			}
			#endregion

			#region Update
			[Test]
			public void Update_When_called_for_entity_without_auditable_interface_Then_calls_entityStateManager_get()
			{
				var stubEntity = new StubDomainEntity();
				var stubDbEntity = new StubDbEntity();
				_entityStateManagerMock.Setup(x => x.Get(stubEntity)).Returns(stubDbEntity);

				_repository.Update(stubEntity);
				
				_entityStateManagerMock.Verify(x => x.Get(stubEntity), Times.Once);
			}
			[Test]
			public void Update_When_called_for_not_attached_entity_Then_throws_entityPersistenceException()
			{
				var stubEntity = new StubDomainEntity();

				Assert.Throws<EntityPersistenceException>(() => _repository.Update(stubEntity));
			}

			[Test]
			public void Update_When_called_for_entity_without_auditable_interface_Then_calls_entityMapper_applyChanges_for_dbEntity()
			{
				var stubEntity = new StubDomainEntity();
				var stubDbEntity = new StubDbEntity();
				_entityStateManagerMock.Setup(x => x.Get(stubEntity)).Returns(stubDbEntity);

				_repository.Update(stubEntity);
				
				_dbEntityMapperMock.Verify(x => x.ApplyChanges(stubEntity, stubDbEntity));
			}

			[Test]
			public void Update_When_called_for_entity_collection_without_auditable_interface_Then_calls_entityStateManager_get_for_each_item()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity {Id = 1, Text = "hello"},
					new StubDomainEntity {Id = 2, Text = "world"},
					new StubDomainEntity {Id = 3, Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity {Id = 1, PropertyText = "hello"},
					new StubDbEntity {Id = 2, PropertyText = "world"},
					new StubDbEntity {Id = 3, PropertyText = "another"}
				};

				for (var i = 0; i < entityCollection.Count; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(index))).Returns(dbEntityCollection.ElementAt(index));
				}

				_repository.Update(entityCollection);

				foreach (var entity in entityCollection)
				{
					var localEntity = entity;
					_entityStateManagerMock.Verify(x => x.Get(localEntity), Times.Once);
				}
			}

			[Test]
			public void Update_When_called_for_entity_collection_with_detached_entity_Then_throws_entityPersistenceException()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity {Id = 1, Text = "hello"},
					new StubDomainEntity {Id = 2, Text = "world"},
					new StubDomainEntity {Id = 3, Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity {Id = 1, PropertyText = "hello"},
					new StubDbEntity {Id = 2, PropertyText = "world"},
					new StubDbEntity {Id = 3, PropertyText = "another"}
				};

				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(1))).Returns(dbEntityCollection.ElementAt(1));
				_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(2))).Returns(dbEntityCollection.ElementAt(2));

				Assert.Throws<EntityPersistenceException>(() => _repository.Update(entityCollection));
			}


			[Test]
			public void Update_When_called_for_entity_collection_without_auditable_interface_Then_calls_entityMapper_applyChanges_for_each_item()
			{
				var entityCollection = new List<StubDomainEntity>
				{
					new StubDomainEntity {Id = 1, Text = "hello"},
					new StubDomainEntity {Id = 2, Text = "world"},
					new StubDomainEntity {Id = 3, Text = "another"}
				};

				var dbEntityCollection = new List<StubDbEntity>
				{
					new StubDbEntity {Id = 1, PropertyText = "hello"},
					new StubDbEntity {Id = 2, PropertyText = "world"},
					new StubDbEntity {Id = 3, PropertyText = "another"}
				};

				for (var i = 0; i < entityCollection.Count; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(x => x.Get(entityCollection.ElementAt(index))).Returns(dbEntityCollection.ElementAt(index));
				}

				_repository.Update(entityCollection);

				for (var i = 0; i < entityCollection.Count; i++)
				{
					var index = i;
					_dbEntityMapperMock.Verify(
						x => x.ApplyChanges(entityCollection.ElementAt(index), dbEntityCollection.ElementAt(index)), Times.Once);
				}
			}
			#endregion

			public class StubDomainEntity : IDomainEntity
			{
				public int Id { get; set; }
				public string Text { get; set; }
			}

			public class StubDbEntity : IDbEntity
			{
				public int Id { get; set; }
				public string PropertyText { get; set; }
				public string TrimmedText { get; set; }
			}
		}

		public class For_ILogicallyDeletable
		{
			private readonly IRepository<StubLogicallyDeletableDomainEntity> _repository;

			private Mock<IDbSet<StubLogicallyDeletableDbEntity>> _dbSetMock = new Mock<IDbSet<StubLogicallyDeletableDbEntity>>();
			private Mock<IDbEntityStateKeeper<StubLogicallyDeletableDomainEntity, StubLogicallyDeletableDbEntity>> _entityStateManagerMock = new Mock<IDbEntityStateKeeper<StubLogicallyDeletableDomainEntity, StubLogicallyDeletableDbEntity>>();
			private Mock<IDbEntityMapper> _dbEntityMapperMock = new Mock<IDbEntityMapper>();

			public For_ILogicallyDeletable()
			{
				_repository = new Repository<StubLogicallyDeletableDomainEntity, StubLogicallyDeletableDbEntity>(_dbSetMock.Object, _entityStateManagerMock.Object, _dbEntityMapperMock.Object);
			}

			#region Delete
			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_Then_sets_IsDeleted_DeletedAt()
			{
				var stubEntity = new StubLogicallyDeletableDomainEntity();
				var stubDbEntity = new StubLogicallyDeletableDbEntity();
				
				_entityStateManagerMock.Setup(x => x.Get(stubEntity))
					.Returns(stubDbEntity);

				_repository.Delete(stubEntity);

				Assert.IsTrue(stubDbEntity.IsDeleted);
				Assert.IsNotNull(stubDbEntity.DeletedAt);
			}

			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_Then_calls_entityStateManager_remove()
			{
				var stubEntity = new StubLogicallyDeletableDomainEntity();
				var stubDbEntity = new StubLogicallyDeletableDbEntity();
				
				_entityStateManagerMock.Setup(x => x.Get(stubEntity))
					.Returns(stubDbEntity);

				_repository.Delete(stubEntity);

				_entityStateManagerMock.Verify(x => x.Remove(stubEntity));
			}
			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_Then_changes_entity_state_to_updated()
			{
				var stubEntity = new StubLogicallyDeletableDomainEntity();
				var stubDbEntity = new StubLogicallyDeletableDbEntity();
				
				_entityStateManagerMock.Setup(x => x.Get(stubEntity))
					.Returns(stubDbEntity);

				_repository.Delete(stubEntity);

				_dbEntityMapperMock.Verify(x=>x.ApplyChanges(stubEntity, stubDbEntity));
			}

			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_that_is_already_deleted_Then_throws_entityDeletedException()
			{
				var stubEntity = new StubLogicallyDeletableDomainEntity
				{
				};

				var stubDbEntity = new StubLogicallyDeletableDbEntity
				{
					IsDeleted = true,
					DeletedAt = DateTime.UtcNow.AddDays(-1)
				};

				_entityStateManagerMock.Setup(x => x.Get(stubEntity))
					.Returns(stubDbEntity);

				Assert.Throws<EntityDeletedException>(() => _repository.Delete(stubEntity));
			}

			[Test]
			public void
				Delete_When_called_for_collection_of_entities_with_logically_deletable_interface_Then_sets_IsDeleted_DeletedAt_for_each_item_in_collection
				()
			{
				var collection = new List<StubLogicallyDeletableDomainEntity>
				{
					new StubLogicallyDeletableDomainEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 3
					}
				};

				var dbEntityCollection = new List<StubLogicallyDeletableDbEntity>
				{
					new StubLogicallyDeletableDbEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 3
					}
				};
				for (var i = 0; i < collection.Count; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(
						x => x.Get(collection.ElementAt(index)))
						.Returns(dbEntityCollection.ElementAt(index));

				}

				_repository.Delete(collection);

				for (var i = 0; i < collection.Count; i++)
				{
					var index = i;
					var entity = collection.ElementAt(index);
					var dbEntity = dbEntityCollection.ElementAt(index);

					Assert.IsTrue(dbEntity.IsDeleted);
					Assert.GreaterOrEqual(dbEntity.DeletedAt, DateTime.UtcNow.AddSeconds(-1));
				}
			}

			[Test]
			public void
				Delete_When_called_for_collection_of_entities_with_logically_deletable_interface_Then_changes_entity_state_to_updated_for_each_item_in_collection
				()
			{
				var collection = new List<StubLogicallyDeletableDomainEntity>
				{
					new StubLogicallyDeletableDomainEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 3
					}
				};

				var dbEntityCollection = new List<StubLogicallyDeletableDbEntity>
				{
					new StubLogicallyDeletableDbEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 3
					}
				};

				for (var i = 0; i < collection.Count; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(
						x => x.Get(collection.ElementAt(index)))
						.Returns(dbEntityCollection.ElementAt(index));

				}

				_repository.Delete(collection);

				for (var i = 0; i < collection.Count; i++)
				{
					var index = i;
					var entity = collection.ElementAt(index);
					var dbEntity = dbEntityCollection.ElementAt(index);

					_dbEntityMapperMock.Verify(x => x.ApplyChanges(entity, dbEntity));
				}
			}

			[Test]
			public void Delete_When_called_for_collection_of_entities_that_has_already_deleted_entity_Then_throws()
			{
				var collection = new List<StubLogicallyDeletableDomainEntity>
				{
					new StubLogicallyDeletableDomainEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDomainEntity
					{
						Id = 3
					}
				};

				var dbEntityCollection = new List<StubLogicallyDeletableDbEntity>
				{
					new StubLogicallyDeletableDbEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableDbEntity
					{
						Id = 3,
						IsDeleted = true
					}
				};

				for (var i = 0; i < collection.Count; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(
						x => x.Get(collection.ElementAt(index)))
						.Returns(dbEntityCollection.ElementAt(index));

				}

				Assert.Throws<EntityDeletedException>(() => _repository.Delete(collection));
			}
			#endregion

			public class StubLogicallyDeletableDbEntity : IDbEntity, ILogicallyDeletable
			{
				public int Id { get; set; }
				public bool IsDeleted { get; set; }
				public DateTime? DeletedAt { get; set; }
			}

			public class StubLogicallyDeletableDomainEntity : IDomainEntity, ILogicallyDeletable
			{
				public int Id { get; set; }
				public bool IsDeleted { get; set; }
				public DateTime? DeletedAt { get; set; }
			}
		}

		public class For_IAuditable
		{
			private readonly IRepository<StubAuditableDomainEntity> _repository;

			private Mock<IDbSet<StubAuditableDbEntity>> _dbSetMock = new Mock<IDbSet<StubAuditableDbEntity>>();
			private Mock<IDbEntityStateKeeper<StubAuditableDomainEntity, StubAuditableDbEntity>> _entityStateManagerMock = new Mock<IDbEntityStateKeeper<StubAuditableDomainEntity, StubAuditableDbEntity>>();
			private Mock<IDbEntityMapper> _dbEntityMapperMock = new Mock<IDbEntityMapper>();

			public For_IAuditable()
			{
				_repository = new Repository<StubAuditableDomainEntity, StubAuditableDbEntity>(_dbSetMock.Object, _entityStateManagerMock.Object, _dbEntityMapperMock.Object);
			}

			#region Add
			[Test]
			public void Add_When_called_Then_updates_createdAt()
			{
				var entity = new StubAuditableDomainEntity
				{
				};

				_repository.Add(entity);

				Assert.GreaterOrEqual(entity.CreatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Add_When_called_Then_updates_lastUpdatedAt()
			{
				var entity = new StubAuditableDomainEntity
				{
				};

				_repository.Add(entity);

				Assert.GreaterOrEqual(entity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Add_When_called_with_collection_Then_updates_createdAt_for_each_entity()
			{
				var collection = new List<StubAuditableDomainEntity>
				{
					new StubAuditableDomainEntity
					{
						Number = 15
					},
					new StubAuditableDomainEntity
					{
						Number = 16
					},
					new StubAuditableDomainEntity
					{
						Number = 17
					}
				};

				_repository.Add(collection);

				foreach (var entity in collection)
				{
					Assert.GreaterOrEqual(entity.CreatedAt, DateTime.UtcNow.AddSeconds(-1));
				}
			}
			#endregion

			#region Update
			[Test]
			public void Update_When_called_for_auditable_entity_Then_does_not_update_createdAt()
			{
				var entityCreatedAt = DateTime.UtcNow.AddDays(-1);

				var entity = new StubAuditableDomainEntity
				{
					Id = 15,
					CreatedAt = entityCreatedAt,
					LastUpdatedAt = DateTime.UtcNow.AddDays(-3)
				};

				var dbEntity = new StubAuditableDbEntity
				{
					Id = 15,
					CreatedAt = entityCreatedAt,
					LastUpdatedAt = DateTime.UtcNow.AddDays(-3)
				};

				_entityStateManagerMock.Setup(x => x.Get(entity)).Returns(dbEntity);

				_repository.Update(entity);

				Assert.AreEqual(entityCreatedAt, entity.CreatedAt);
			}

			[Test]
			public void Update_When_called_for_auditable_entity_Then_updates_lastUpdatedAt()
			{
				var entity = new StubAuditableDomainEntity
				{
					Id = 15,
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddHours(-1)
				};

				var dbEntity = new StubAuditableDbEntity
				{
					Id = 15,
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddDays(-3)
				};

				_entityStateManagerMock.Setup(x => x.Get(entity)).Returns(dbEntity);

				_repository.Update(entity);

				Assert.GreaterOrEqual(dbEntity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Update_When_called_for_collection_of_previously_updated_entities_Then_updates_lastUpdatedAt_for_each_entity()
			{
				var entities = new[]
				{
					new StubAuditableDomainEntity
					{
						Id = 15,
						CreatedAt = DateTime.UtcNow.AddDays(-1),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-1)
					},

					new StubAuditableDomainEntity
					{
						Id = 16,
						CreatedAt = DateTime.UtcNow.AddDays(-3),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
					},

					new StubAuditableDomainEntity
					{
						Id = 17,
						CreatedAt = DateTime.UtcNow.AddDays(-4),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
					}
				};

				var dbEntities =
					new[]
					{
						new StubAuditableDbEntity
						{
							Id = 15,
							CreatedAt = DateTime.UtcNow.AddDays(-1),
							LastUpdatedAt = DateTime.UtcNow.AddHours(-1)
						},

						new StubAuditableDbEntity
						{
							Id = 16,
							CreatedAt = DateTime.UtcNow.AddDays(-3),
							LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
						},

						new StubAuditableDbEntity
						{
							Id = 17,
							CreatedAt = DateTime.UtcNow.AddDays(-4),
							LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
						}
					};

				for (var i = 0; i < entities.Length; i++)
				{
					var index = i;
					_entityStateManagerMock.Setup(x => x.Get(entities[index])).Returns(dbEntities[index]);
				}

				_repository.Update(entities);

				for (var i = 0; i < entities.Length; i++)
				{
					var entity = dbEntities[i];
					Assert.GreaterOrEqual(entity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
				}
			}
			#endregion

			public class StubAuditableDbEntity : IDbEntity, IAuditable
			{
				public int Id { get; set; }

				public int Number { get; set; }

				public DateTime CreatedAt { get; set; }
				public DateTime LastUpdatedAt { get; set; }
			}

			public class StubAuditableDomainEntity : IDomainEntity, IAuditable
			{
				public int Id { get; set; }

				public int Number { get; set; }

				public DateTime CreatedAt { get; set; }
				public DateTime LastUpdatedAt { get; set; }
			}

		}
	}
}