using AutoMapper;
using ENTech.Store.Services.External.ForCustomer.StoreModule.MapProfiles;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.PartnerModule.Requests;
using ENTech.Store.Services.Internal.StoreModule;
using InternalProductFindRequest = ENTech.Store.Services.Internal.StoreModule.Requests.ProductFindRequest;
using InternalProductFindResponse = ENTech.Store.Services.Internal.StoreModule.Responses.ProductFindResponse;
using ExternalProductFindRequest = ENTech.Store.Services.External.ForCustomer.StoreModule.Requests.ProductFindRequest;
using ExternalProductFindResponse = ENTech.Store.Services.External.ForCustomer.StoreModule.Responses.ProductFindResponse;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Commands
{
	public class ProductFindCommand : PartnerAuthExternalCommandBase<ExternalProductFindRequest, ExternalProductFindResponse, InternalProductFindRequest, InternalProductFindResponse>
	{
		protected override bool RequireTransaction
		{
			get { return false; }
		}

		private readonly IProductService _productService;

		public ProductFindCommand(IProductService productService, IPartnerService partnerService) : base(partnerService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override InternalProductFindResponse ExecuteInternal(InternalProductFindRequest internalRequest)
		{
			return _productService.Find(internalRequest);
		}

		protected override void Authorize(ExternalProductFindRequest request, ExternalProductFindResponse response)
		{
			//_partnerId;
		}
	}
}
