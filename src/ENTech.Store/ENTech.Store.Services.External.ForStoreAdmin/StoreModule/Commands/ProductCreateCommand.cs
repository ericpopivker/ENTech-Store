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
	internal class ProductCreateCommand :
		PartnerUserAuthExternalCommandBase
			<Requests.ProductCreateRequest, ProductCreateResponse, Internal.StoreModule.Requests.ProductCreateRequest, ProductCreateOrUpdateResponse>
	{
		protected override bool RequireTransaction
		{
			get { return true; }
		}

		private readonly IProductService _productService;

		public ProductCreateCommand(IProductService productService, IPartnerService partnerService, IUserService userService):base(partnerService,userService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override ProductCreateOrUpdateResponse ExecuteInternal(Internal.StoreModule.Requests.ProductCreateRequest internalRequest)
		{
			return _productService.Create(internalRequest);
		}

		protected override void Authorize(Requests.ProductCreateRequest request, ProductCreateResponse response)
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