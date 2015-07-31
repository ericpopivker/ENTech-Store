using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.SharedModule.Requests;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.Expand
{
	public class DtoExpanderTests
	{
		private IDtoExpander _dtoExpander;

		private Mock<IDtoExpanderEngine> _dtoExpanderEngine = new Mock<IDtoExpanderEngine>();
		private Mock<IExternalCommandService> _externalCommandServiceMock = new Mock<IExternalCommandService>();
		private Mock<IMapper> _mapperMock = new Mock<IMapper>();
		
		public DtoExpanderTests()
		{
			_dtoExpanderEngine.Setup(x => x.GetConfiguration(typeof(ProductExpandableStubDto))).Returns(new ProductExpandableStubDtoExpandProfile().GetConfiguration());
			_dtoExpanderEngine.Setup(x => x.GetConfiguration(typeof(ProductGroupExpandableStubDto))).Returns(new ProductGroupExpandableStubDtoExpandProfile().GetConfiguration());

			_externalCommandServiceMock.Setup(x => x.Execute(It.IsAny<ProductStubDtoGetByIdRequest>()))
				.Returns(new OkResponseStatus<ProductStubDtoGetByIdResponse>(new ProductStubDtoGetByIdResponse
				{
					Item = new ProductStubDto
					{
						Id = 1,
						Name = "Test",
						PropertyId = 15,
						CollectionIds = new[] {1, 2, 3, 4}
					}
				}));

			_externalCommandServiceMock.Setup(x => x.Execute(It.IsAny<ProductCategoryStubGetByIdRequest>()))
				.Returns(new OkResponseStatus<ProductCategoryStubGetByIdResponse>(new ProductCategoryStubGetByIdResponse()
				{
					Item = new ProductCategoryStubDto()
					{
						Id = 1,
						Text = "Loaded",
						ModeratorId = 3
					}
				}));
			_externalCommandServiceMock.Setup(x => x.Execute(It.IsAny<ProductCategoryModeratorStubGetByIdRequest>()))
				.Returns((ProductCategoryModeratorStubGetByIdRequest request) => new OkResponseStatus<ProductCategoryModeratorStubGetByIdResponse>(new ProductCategoryModeratorStubGetByIdResponse()
				{
					Item = new ProductCategoryModeratorStubDto()
					{
						Id = request.Id,
						FirstName = "Test",
						LastName = "Moderator"
					}
				}));

			_externalCommandServiceMock.Setup(x => x.Execute(It.IsAny<ProductCategoryStubFindByIdsRequest>()))
				.Returns(new OkResponseStatus<ProductCategoryStubFindByIdsResponse>(new ProductCategoryStubFindByIdsResponse()
				{
					Items = new []{new ProductCategoryStubDto()
					{
						Id = 1,
						Text = "Loaded"
					}, new ProductCategoryStubDto
					{
						Id = 2,
						Text = "Second Loaded"
					}, new ProductCategoryStubDto
					{
						Id = 3,
						Text = "Third Loaded"
					} , new ProductCategoryStubDto
					{
						Id = 4,
						Text = "Fourth Loaded"
					} 
					}
				}));

			_mapperMock.Setup(x => x.Map(typeof (ProductStubDto), typeof (ProductExpandableStubDto), It.IsAny<object>()))
				.Returns((Type sourceType, Type targetType, object result) =>
				{
					var cast = (ProductStubDto) result;
					return (object) new ProductExpandableStubDto
					{
						Id = cast.Id,
						Name = cast.Name,
						MainCategoryId = cast.PropertyId,
						AdditionalCategoryIds = cast.CollectionIds.ToArray()
					};
				});

			_mapperMock.Setup(x => x.Map(typeof (ProductCategoryStubDto), typeof (ProductGroupExpandableStubDto), It.IsAny<object>()))
				.Returns((Type sourceType, Type targetType, object result) =>
				{
					var cast = (ProductCategoryStubDto)result;
					return (object)new ProductGroupExpandableStubDto
					{
						Id = cast.Id,
						Text = cast.Text
					};
				});

			_mapperMock.Setup(x => x.Map(typeof (ProductCategoryModeratorStubDto), typeof (ProductCategoryModeratorExpandedStubDto), It.IsAny<object>()))
				.Returns((Type sourceType, Type targetType, object result) =>
				{
					var cast = (ProductCategoryModeratorStubDto)result;
					return (object)new ProductCategoryModeratorExpandedStubDto
					{
						Id = cast.Id,
						FullName = cast.FirstName + " " + cast.LastName
					};
				});

			_mapperMock.Setup(x => x.MapCollection(typeof (ProductCategoryStubDto), typeof (ProductGroupExpandableStubDto), It.IsAny<IEnumerable<object>>()))
				.Returns((Type sourceType, Type targetType, IEnumerable<object> result) =>
				{
					var cast = result.Cast<ProductCategoryStubDto>();
					return cast.Select(x => (object) new ProductGroupExpandableStubDto
					{
						Id = x.Id,
						Text = x.Text
					});
				});

			_dtoExpander = new DtoExpander(_dtoExpanderEngine.Object, _externalCommandServiceMock.Object, _mapperMock.Object);
		}

		[TearDown]
		public void TearDown()
		{
		}

		[Test] 
		public void LoadAndExpand_When_called_Then_gets_all_configurations_from_engine()
		{
			var result = _dtoExpander.LoadAndExpand<ProductExpandableStubDto>(1);
		}

		public class ProductExpandableStubDtoExpandProfile : ExpandProfile<ProductExpandableStubDto>
		{
			protected override void Configure()
			{
				LoadRoot()
					.IfSingle<ProductStubDtoGetByIdRequest>()
					.IfMultiple<ProductStubDtoFindByIdsRequest>();

				ExpandProperty(x => x.MainCategory)
					.FromIdentityProperty(x => x.MainCategoryId)
					.IfSingle<ProductCategoryStubGetByIdRequest>()
					.IfMultiple<ProductCategoryStubFindByIdsRequest>();

				ExpandProperty(x => x.AdditionalCategories)
					.FromIdentityProperty(x => x.AdditionalCategoryIds)
					.IfSingle<ProductCategoryStubGetByIdRequest>()
					.IfMultiple<ProductCategoryStubFindByIdsRequest>();
			}
		}

		public class ProductGroupExpandableStubDtoExpandProfile : ExpandProfile<ProductGroupExpandableStubDto>
		{
			protected override void Configure()
			{
				LoadRoot()
					.IfSingle<ProductCategoryStubGetByIdRequest>()
					.IfMultiple<ProductCategoryStubFindByIdsRequest>();

				ExpandProperty(x => x.Moderator)
					.FromIdentityProperty(x => x.ModeratorId)
					.IfSingle<ProductCategoryModeratorStubGetByIdRequest>()
					.IfMultiple<ProductCategoryModeratorStubFindByIdsRequest>();
			}
		}

		public class ExpandableStubExpandOption : ExpandOption<ProductExpandableStubDto>
		{
			
		}

		public class AnotherExpandableStubExpandOption : ExpandOption<ProductExpandableStubDto>
		{
			
		}

		public class ProductExpandableStubDto : IExpandableDto
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public int MainCategoryId { get; set; }
			public ProductGroupExpandableStubDto MainCategory { get; set; }

			public int[] AdditionalCategoryIds { get; set; }
			public List<ProductGroupExpandableStubDto> AdditionalCategories { get; set; }
		}

		public class ProductGroupExpandableStubDto : IExpandableDto
		{
			public int Id { get; set; }
			public string Text { get; set; }

			public int ModeratorId { get; set;}
			public ProductCategoryModeratorExpandedStubDto Moderator { get; set; }
		}

		public class ProductCategoryModeratorExpandedStubDto
		{
			public int Id { get; set; }
			public string FullName { get; set; }
		}

		public class ProductStubDto
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int PropertyId { get; set; }
			public IEnumerable<int> CollectionIds { get; set; }
		}

		public class ProductCategoryStubDto
		{
			public int Id { get; set; }
			public string Text { get; set; }
			public int ModeratorId { get; set; }
		}
		

		public class ProductCategoryModeratorStubDto
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		public class ProductStubDtoGetByIdRequest : GetByIdRequestBase<ProductStubDtoGetByIdResponse>
		{
		}


		public class ProductStubDtoGetByIdResponse : GetByIdResponse<ProductStubDto>
		{

		}

		public class ProductStubDtoFindByIdsRequest : FindByIdsRequestBase<ProductStubDtoFindByIdsResponse>
		{
		}


		public class ProductStubDtoFindByIdsResponse : FindResponse<ProductStubDto>
		{

		}

		public class ProductCategoryStubGetByIdRequest : GetByIdRequestBase<ProductCategoryStubGetByIdResponse>
		{
		}


		public class ProductCategoryStubGetByIdResponse : GetByIdResponse<ProductCategoryStubDto>
		{

		}

		public class ProductCategoryStubFindByIdsRequest : FindByIdsRequestBase<ProductCategoryStubFindByIdsResponse>
		{
		}
		
		public class ProductCategoryStubFindByIdsResponse : FindResponse<ProductCategoryStubDto>
		{

		}

		public class ProductCategoryModeratorStubGetByIdRequest : GetByIdRequestBase<ProductCategoryModeratorStubGetByIdResponse>
		{
		}

		public class ProductCategoryModeratorStubGetByIdResponse : GetByIdResponse<ProductCategoryModeratorStubDto>
		{

		}

		public class ProductCategoryModeratorStubFindByIdsRequest : FindByIdsRequestBase<ProductCategoryModeratorStubFindByIdsResponse>
		{
		}

		public class ProductCategoryModeratorStubFindByIdsResponse : FindResponse<ProductCategoryModeratorStubDto>
		{

		}
	}
}
