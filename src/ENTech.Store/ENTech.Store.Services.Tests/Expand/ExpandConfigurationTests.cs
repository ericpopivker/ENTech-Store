using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.SharedModule.Requests;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.Expand
{
	public class ExpandConfigurationTests
	{
		[Test]
		public void Test()
		{
			var config = new ExpandableDtoExpandProfile();
		}

		public class ExpandableDtoExpandProfile : ExpandProfile<ExpandableDto>
		{
			protected override void Configure()
			{
				LoadRoot()
				.IfSingle<ExpandableDtoGetByIdResponse>()
				.IfMultiple<ExpandableDtoFindByIdsRequest>();

				ExpandProperty(x => x.Property)
					.FromIdentityProperty(x => x.PropertyId)
					.IfSingle<StubPropertyGetByIdRequest>()
					.IfMultiple<StubPropertyFindByIdsRequest>();
			}
		}


		public class ExpandableDto : IExpandableDto
		{
			public string Name { get; set; }
			public int PropertyId { get; set; }
			public StubProperty Property { get; set; }
		}

		public class StubProperty
		{
		}

		public class StubDto
		{
		}

		public class StubPropertyDto
		{
			
		}

		public class ExpandableDtoGetByIdRequest : GetByIdRequestBase<ExpandableDtoGetByIdResponse>
		{
		}


		public class ExpandableDtoGetByIdResponse : GetByIdResponse<StubDto>
		{

		}

		public class ExpandableDtoFindByIdsRequest : FindByIdsRequestBase<ExpandableDtoFindByIdsResponse>
		{
		}


		public class ExpandableDtoFindByIdsResponse : GetByIdResponse<StubDto>
		{

		}

		public class StubPropertyGetByIdRequest : GetByIdRequestBase<StubPropertyGetByIdResponse>
		{
		}


		public class StubPropertyGetByIdResponse : GetByIdResponse<StubPropertyDto>
		{

		}

		public class StubPropertyFindByIdsRequest : FindByIdsRequestBase<StubPropertyFindByIdsResponse>
		{
		}


		public class StubPropertyFindByIdsResponse : GetByIdResponse<StubPropertyDto>
		{

		}

		
	}
}