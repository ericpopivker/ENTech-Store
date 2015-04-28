using ENTech.Store.Entities;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.PartnerModule.Requests;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using IDbContext = ENTech.Store.Entities.IDbContext;


namespace ENTech.Store.Services.External.ForCustomer.UnitTests
{
	[TestFixture]
	public class PartnerAuthExternalCommandBaseTest
	{
		public class PartnerAuthExternalCommandStub : PartnerAuthExternalCommandBase<PartnerAuthExternalCommandStub.ExternalRequestStub, ExternalResponse, PartnerAuthExternalCommandStub.InternalRequestStub, InternalResponse>
		{
			public class InternalRequestStub : IInternalRequest
			{}

			public class ExternalRequestStub : ExternalRequestBase
			{ }

			public PartnerAuthExternalCommandStub(IPartnerService partnerService) : base(partnerService)
			{
			}

			protected override void LimitDbContext(IDbContext dbContext)
			{
				//throw new System.NotImplementedException();
			}

			public override InternalResponse ExecuteInternal(InternalRequestStub internalRequest)
			{
				//throw new System.NotImplementedException();
				return new InternalResponse();
			}
		}


		[Test]
		public void Execute_Then_()
		{
		}

	}
}
