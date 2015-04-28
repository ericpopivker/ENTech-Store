using System.Linq;
using AutoMapper;
using ENTech.Store.Core.UserModule;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.MapProfiles;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Requests;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Responses;
using ENTech.Store.Services.Internal.CustomerModule;
using ENTech.Store.Services.Internal.PartnerModule;
using ENTech.Store.Services.Internal.StoreModule;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Commands
{
	public class ProductDeleteCommand : PartnerUserAuthExternalCommandBase<ProductDeleteRequest, ProductDeleteResponse, Internal.StoreModule.Requests.ProductDeleteRequest, Internal.StoreModule.Responses.ProductDeleteResponse>
	{
		protected override bool RequireTransaction
		{
			get { return false; }
		}

		private readonly IProductService _productService;

		public ProductDeleteCommand(IProductService productService, IPartnerService partnerService, IUserService userService)
			: base(partnerService, userService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override Internal.StoreModule.Responses.ProductDeleteResponse ExecuteInternal(Internal.StoreModule.Requests.ProductDeleteRequest internalRequest)
		{
			return _productService.Delete(internalRequest);
		}

		protected override void Authorize(ProductDeleteRequest request, ProductDeleteResponse response)
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
