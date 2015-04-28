using System.Linq;
using AutoMapper;
using ENTech.Store.Core.UserModule;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.MapProfiles;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Responses;
using ENTech.Store.Services.Internal.CustomerModule;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.StoreModule;
using ENTech.Store.Services.Internal.StoreModule.Responses;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Commands
{
	internal class ProductUpdateCommand :
		PartnerUserAuthExternalCommandBase
			<Requests.ProductUpdateRequest, ProductUpdateResponse, Internal.StoreModule.Requests.ProductUpdateRequest, ProductCreateOrUpdateResponse>
	{
		protected override bool RequireTransaction
		{
			get { return true; }
		}

		private readonly IProductService _productService;

		public ProductUpdateCommand(IProductService productService, IPartnerService partnerService, IUserService userService):base(partnerService,userService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override ProductCreateOrUpdateResponse ExecuteInternal(Internal.StoreModule.Requests.ProductUpdateRequest internalRequest)
		{
			return _productService.Update(internalRequest);
		}

		protected override void Authorize(Requests.ProductUpdateRequest request, ProductUpdateResponse response)
		{
			var user = UnitOfWork.DbContext.Users.Single(u => u.Id == UserId);
			if (user.Type != UserType.StoreAdmin)
			{
				ErrorResponse(response, CommonErrorCode.UserNotAuthorized);
			}
			if (response.IsSuccess && request.StoreId != StoreId)
			{
				ErrorResponse(response, CommonErrorCode.UserNotAuthorized);
			}
		}
	}
}