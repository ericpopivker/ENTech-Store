using AutoMapper;
using ENTech.Store.Services.External.ForCustomer.StoreModule.MapProfiles;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.StoreModule;
using InternalProductGetByIdRequest = ENTech.Store.Services.Internal.StoreModule.Requests.ProductGetByIdRequest;
using InternalProductGetByIdResponse = ENTech.Store.Services.Internal.StoreModule.Responses.ProductCreateOrUpdateResponse;
using ExternalProductGetByIdRequest = ENTech.Store.Services.External.ForCustomer.StoreModule.Requests.ProductGetByIdRequest;
using ExternalProductGetByIdResponse = ENTech.Store.Services.External.ForCustomer.StoreModule.Responses.ProductGetByIdResponse;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Commands
{
	class ProductGetByIdCommand : PartnerAuthExternalCommandBase<ExternalProductGetByIdRequest, ExternalProductGetByIdResponse, InternalProductGetByIdRequest, InternalProductGetByIdResponse>
	{
		protected override bool RequireTransaction
		{
			get { return false; }
		}

		private readonly IProductService _productService;

		public ProductGetByIdCommand(IProductService productService, IPartnerService partnerService) : base(partnerService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override InternalProductGetByIdResponse ExecuteInternal(InternalProductGetByIdRequest internalRequest)
		{
			return _productService.GetById(internalRequest);
		}

		protected override void Authorize(ExternalProductGetByIdRequest request, ExternalProductGetByIdResponse response)
		{
		}
	}
}
