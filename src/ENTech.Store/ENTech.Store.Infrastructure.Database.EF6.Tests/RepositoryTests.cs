using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Database.Exceptions;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Database.EF6.Tests
{
	public class RepositoryTests
	{
		public class ForIEntity
		{
			private readonly Repository<StubEntity> _repository;
			private readonly ObservableCollection<StubEntity> _dbSetData;

			private Mock<IDbSet<StubEntity>> _dbSetMock = new Mock<IDbSet<StubEntity>>();
			private Mock<IDbEntityStateManager<StubEntity>> _entityStateManagerMock = new Mock<IDbEntityStateManager<StubEntity>>();

			private readonly StubEntity _firstEntity = new StubEntity { Id = 1 };
			private readonly StubEntity _secondEntity = new StubEntity { Id = 2 };
			private readonly StubEntity _thirdEntity = new StubEntity { Id = 3 };
			private readonly StubEntity _fourthEntityWithDupeId = new StubEntity { Id = 4 };
			private readonly StubEntity _fifthEntityWithDupeId = new StubEntity { Id = 4 };

			public ForIEntity()
			{
				_dbSetData = new ObservableCollection<StubEntity>
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

				_repository = new Repository<StubEntity>(_dbSetMock.Object, _entityStateManagerMock.Object);
			}

			#region Ctor
			[Test]
			public void Ctor_When_called_with_null_dbset_and_entityStateManager_Then_throws_argumentNullException()
			{
				Assert.Throws<ArgumentNullException>(() => new Repository<StubEntity>(null, null));
			}

			[Test]
			public void Ctor_When_called_with_null_dbset_Then_throws_argumentNullException()
			{
				Assert.Throws<ArgumentNullException>(() => new Repository<StubEntity>(null, _entityStateManagerMock.Object));
			}

			[Test]
			public void Ctor_When_called_with_null_entityStateManager_Then_throws_argumentNullException()
			{
				Assert.Throws<ArgumentNullException>(() => new Repository<StubEntity>(_dbSetMock.Object, null));
			}
			#endregion

			#region GetById
			[Test]
			public void GetById_When_called_Then_gets_item_with_correct_id_from_collecion()
			{
				var entityId = _thirdEntity.Id;

				var result = _repository.GetById(entityId);

				Assert.AreEqual(_thirdEntity, result);
			}

			[Test]
			public void GetById_When_called_for_entity_with_dupe_id_Then_does_not_throw()
			{
				var entityId = _fourthEntityWithDupeId.Id;

				Assert.DoesNotThrow(() => _repository.GetById(entityId));
			}
			#endregion

			#region FindByIds
			[Test]
			public void FindByIds_When_called_with_correct_ids_Then_returns_correct_collection_with_items_with_these_ids()
			{
				var entityIds = new[] {_firstEntity.Id, _secondEntity.Id, _thirdEntity.Id};

				var results = _repository.FindByIds(entityIds).ToList();

				Assert.Contains(_firstEntity, results);
				Assert.Contains(_secondEntity, results);
				Assert.Contains(_thirdEntity, results);
			}

			[Test]
			public void FindByIds_When_called_with_ids_some_of_which_can_not_be_found_Then_throws_exception()
			{
				var entityIds = new[] {_firstEntity.Id, _secondEntity.Id, -1};

				Assert.Throws<EntityLoadMismatchException>(() => _repository.FindByIds(entityIds));
			}
			#endregion

			#region Add
			[Test]
			public void Add_When_called_for_entity_Then_calls_dbSet_add()
			{
				var stubEntity = new StubEntity();

				_repository.Add(stubEntity);

				_dbSetMock.Verify(x => x.Add(stubEntity));
			}

			[Test]
			public void Add_When_called_for_multiple_entities_Then_calls_dbSet_add_for_each_entity()
			{
				var entityCollection = new List<StubEntity>
				{
					new StubEntity { Text = "hello"},
					new StubEntity { Text = "world"},
					new StubEntity { Text = "another"}
				};

				_repository.Add(entityCollection);

				foreach (var stubEntity in entityCollection)
				{
					var entity = stubEntity;
					_dbSetMock.Verify(x => x.Add(entity));
				}
			}
			#endregion

			#region Delete
			[Test]
			public void Delete_When_called_for_entity_without_logically_deletable_interface_Then_calls_dbSet_remove()
			{
				var stubEntity = new StubEntity();

				_repository.Delete(stubEntity);

				_dbSetMock.Verify(x => x.Remove(stubEntity));
			}

			[Test]
			public void Delete_When_called_for_multiple_entities_without_logically_deletable_interface_Then_calls_dbSet_remove_for_each_entity()
			{
				var collection = new List<StubEntity>
				{
					new StubEntity {Id = 1, Text = "hello"},
					new StubEntity {Id = 2, Text = "world"},
					new StubEntity {Id = 3, Text = "another"}
				};

				_repository.Delete(collection);

				foreach (var stubEntity in collection)
				{
					var entity = stubEntity;
					_dbSetMock.Verify(x => x.Remove(entity));
				}
			}

			#endregion

			#region Update
			[Test]
			public void Update_When_called_for_entity_without_auditable_interface_Then_calls_entity_state_manager_changeState()
			{
				var stubEntity = new StubEntity();

				_repository.Update(stubEntity);

				_entityStateManagerMock.Verify(x => x.MarkUpdated(stubEntity));
			}

			[Test]
			public void Update_When_called_for_entity_collection_without_auditable_interface_Then_calls_entity_state_manager_changeState_for_each_item()
			{
				var entities = new List<StubEntity>
				{
					new StubEntity(),
					new StubEntity(),
					new StubEntity()
				};

				_repository.Update(entities);

				foreach (var entity in entities)
				{
					var localEntity = entity;
					_entityStateManagerMock.Verify(x => x.MarkUpdated(localEntity));
				}
			}
			#endregion

			public class StubEntity : IEntity
			{
				public int Id { get; set; }
				public string Text { get; set; }
			}
		}

		public class For_ILogicallyDeletable
		{
			private readonly IRepository<StubLogicallyDeletableEntity> _repository;

			private Mock<IDbSet<StubLogicallyDeletableEntity>> _dbSetMock = new Mock<IDbSet<StubLogicallyDeletableEntity>>();
			private Mock<IDbEntityStateManager<StubLogicallyDeletableEntity>> _entityStateManagerMock = new Mock<IDbEntityStateManager<StubLogicallyDeletableEntity>>();

			public For_ILogicallyDeletable()
			{
				_repository = new Repository<StubLogicallyDeletableEntity>(_dbSetMock.Object, _entityStateManagerMock.Object);
			}

			#region Delete
			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_Then_sets_IsDeleted_DeletedAt_and_changes_entity_state_to_updated()
			{
				var stubEntity = new StubLogicallyDeletableEntity();

				_repository.Delete(stubEntity);

				_entityStateManagerMock.Verify(x => x.MarkUpdated(stubEntity));

				Assert.IsTrue(stubEntity.IsDeleted);
				Assert.IsNotNull(stubEntity.DeletedAt);
			}

			[Test]
			public void Delete_When_called_for_entity_with_logically_deletable_interface_that_is_already_deleted_Then_throws_entityDeletedException()
			{
				var stubEntity = new StubLogicallyDeletableEntity
				{
					IsDeleted = true,
					DeletedAt = DateTime.UtcNow.AddDays(-1)
				};

				Assert.Throws<EntityDeletedException>(() => _repository.Delete(stubEntity));

			}

			[Test]
			public void
				Delete_When_called_for_collection_of_entities_with_logically_deletable_interface_Then_sets_IsDeleted_DeletedAt_and_changes_entity_state_to_updated_for_each_item_in_collection
				()
			{
				var collection = new List<StubLogicallyDeletableEntity>
				{
					new StubLogicallyDeletableEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableEntity
					{
						Id = 3
					}
				};

				_repository.Delete(collection);

				foreach (var stubEntity in collection)
				{
					_entityStateManagerMock.Verify(x => x.MarkUpdated(stubEntity));

					Assert.IsTrue(stubEntity.IsDeleted);
					Assert.IsNotNull(stubEntity.DeletedAt);
					
				}
			}

			[Test]
			public void Delete_When_called_for_collection_of_entities_that_has_already_deleted_entity_Then_throws()
			{
				var collection = new List<StubLogicallyDeletableEntity>
				{
					new StubLogicallyDeletableEntity
					{
						Id = 1
					},
					new StubLogicallyDeletableEntity
					{
						Id = 2
					},
					new StubLogicallyDeletableEntity
					{
						Id = 3,
						IsDeleted = true
					}
				};

				Assert.Throws<EntityDeletedException>(() => _repository.Delete(collection));
			}
			#endregion


			public class StubLogicallyDeletableEntity : IEntity, ILogicallyDeletable
			{
				public int Id { get; set; }
				public bool IsDeleted { get; set; }
				public DateTime? DeletedAt { get; set; }
			}
		}

		public class For_IAuditable
		{
			private readonly IRepository<StubAuditableEntity> _repository;

			private Mock<IDbSet<StubAuditableEntity>> _dbSetMock = new Mock<IDbSet<StubAuditableEntity>>();
			private Mock<IDbEntityStateManager<StubAuditableEntity>> _entityStateManagerMock = new Mock<IDbEntityStateManager<StubAuditableEntity>>();

			public For_IAuditable()
			{
				_repository = new Repository<StubAuditableEntity>(_dbSetMock.Object, _entityStateManagerMock.Object);
			}

			#region Add
			[Test]
			public void Add_When_called_Then_updates_createdAt()
			{
				var entity = new StubAuditableEntity
				{
				};

				_repository.Add(entity);

				Assert.GreaterOrEqual(entity.CreatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Add_When_called_Then_updates_lastUpdatedAt()
			{
				var entity = new StubAuditableEntity
				{
				};

				_repository.Add(entity);

				Assert.GreaterOrEqual(entity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Add_When_called_with_collection_Then_updates_createdAt_for_each_entity()
			{
				var collection = new List<StubAuditableEntity>
				{
					new StubAuditableEntity
					{
						Number = 15
					},
					new StubAuditableEntity
					{
						Number = 16
					},
					new StubAuditableEntity
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

				var entity = new StubAuditableEntity
				{
					Id = 15,
					CreatedAt = entityCreatedAt,
					LastUpdatedAt = DateTime.UtcNow.AddDays(-3)
				};

				_repository.Update(entity);

				Assert.AreEqual(entityCreatedAt, entity.CreatedAt);
			}

			[Test]
			public void Update_When_called_for_auditable_entity_Then_updates_lastUpdatedAt()
			{
				var entity = new StubAuditableEntity
				{
					Id = 15,
					CreatedAt = DateTime.UtcNow.AddDays(-1),
					LastUpdatedAt = DateTime.UtcNow.AddHours(-1)
				};

				_repository.Update(entity);

				Assert.NotNull(entity.LastUpdatedAt);
				Assert.GreaterOrEqual(entity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
			}

			[Test]
			public void Update_When_called_for_collection_of_previously_updated_entities_Then_updates_lastUpdatedAt_for_each_entity()
			{
				var entities = new[]
				{
					new StubAuditableEntity
					{
						Id = 15,
						CreatedAt = DateTime.UtcNow.AddDays(-1),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-1)
					},

					new StubAuditableEntity
					{
						Id = 16,
						CreatedAt = DateTime.UtcNow.AddDays(-3),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
					},

					new StubAuditableEntity
					{
						Id = 17,
						CreatedAt = DateTime.UtcNow.AddDays(-4),
						LastUpdatedAt = DateTime.UtcNow.AddHours(-3)
					}
				};

				_repository.Update(entities);

				foreach (var entity in entities)
				{
					Assert.NotNull(entity.LastUpdatedAt);
					Assert.GreaterOrEqual(entity.LastUpdatedAt, DateTime.UtcNow.AddSeconds(-1));
				}
			}
			#endregion

			public class StubAuditableEntity : IEntity, IAuditable
			{
				public int Id { get; set; }

				public int Number { get; set; }

				public DateTime CreatedAt { get; set; }
				public DateTime LastUpdatedAt { get; set; }
			}
			
		}
	}
}