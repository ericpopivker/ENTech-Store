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
	public class ProductFindCommand : PartnerUserAuthExternalCommandBase<ProductFindRequest, ProductFindResponse, Internal.StoreModule.Requests.ProductFindRequest, Internal.StoreModule.Responses.ProductFindResponse>
	{
		protected override bool RequireTransaction
		{
			get { return false; }
		}

		private readonly IProductService _productService;

		public ProductFindCommand(IProductService productService, IPartnerService partnerService, IUserService userService)
			: base(partnerService, userService)
		{
			_productService = productService;

			Mapper.AddProfile<ExternalToInternalRequestMapProfile>();
			Mapper.AddProfile<InternalToExternalResponseMapProfile>();
			Mapper.AssertConfigurationIsValid();
		}

		public override Internal.StoreModule.Responses.ProductFindResponse ExecuteInternal(Internal.StoreModule.Requests.ProductFindRequest internalRequest)
		{
			return _productService.Find(internalRequest);
		}

		protected override void Authorize(ProductFindRequest request, ProductFindResponse response)
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
