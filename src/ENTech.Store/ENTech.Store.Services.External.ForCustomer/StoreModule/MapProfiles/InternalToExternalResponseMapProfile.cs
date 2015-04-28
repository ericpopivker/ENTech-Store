using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Responses;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.MapProfiles
{
	class InternalToExternalResponseMapProfile:Profile
	{
		protected override void Configure()
		{
			CreateMap<InternalResponse, ExternalResponse>()
				.ForMember(e => e.IsSuccess, o => o.MapFrom(i => i.IsSuccess))
				.ForMember(e => e.Error, o => o.MapFrom(i => i.Error))
				.ForMember(e => e.ArgumentErrors, o => o.MapFrom(i => i.ArgumentErrors));

			CreateMap<Internal.StoreModule.Dtos.ProductOptionDto, ProductOptionDto>()
				.ForMember(e => e.Id, o => o.MapFrom(i => i.Id))
				.ForMember(e => e.Index, o => o.MapFrom(i => i.Index))
				.ForMember(e => e.Name, o => o.MapFrom(i => i.Name));

			CreateMap<Internal.StoreModule.Dtos.ProductPhotoDto, ProductPhotoDto>()
				.ForMember(e => e.Id, o => o.MapFrom(i => i.Id))
				.ForMember(e => e.FileName, o => o.MapFrom(i => i.FileName))
				.ForMember(e => e.Url, o => o.MapFrom(i => i.Url));

			CreateMap<Internal.StoreModule.Dtos.ProductVariantOptionValueDto, ProductVariantOptionValueDto>()
				.ForMember(e => e.OptionIndex, o => o.MapFrom(i => i.OptionIndex))
				.ForMember(e => e.OptionValue, o => o.MapFrom(i => i.OptionValue));

			CreateMap<Internal.StoreModule.Dtos.ProductVariantDto, ProductVariantDto>()
				.ForMember(e => e.Id, o => o.MapFrom(i => i.Id))
				.ForMember(e => e.OptionValues, o => o.MapFrom(i => i.OptionValues))
				.ForMember(e => e.Photo, o => o.MapFrom(i => i.Photo))
				.ForMember(e => e.Price, o => o.MapFrom(i => i.Price))
				.ForMember(e => e.QuantityInStock, o => o.MapFrom(i => i.QuantityInStock))
				.ForMember(e => e.SalePrice, o => o.MapFrom(i => i.SalePrice))
				.ForMember(e => e.Sku, o => o.MapFrom(i => i.Sku))
				.ForMember(e => e.Status, o => o.MapFrom(i => i.Status))
				.ForMember(e => e.Weight, o => o.MapFrom(i => i.Weight));

			CreateMap<Internal.StoreModule.Dtos.ProductDto, ProductDto>()
				.ForMember(e => e.CategoryId, o => o.MapFrom(i => i.CategoryId))
				.ForMember(e => e.Description, o => o.MapFrom(i => i.Description))
				.ForMember(e => e.Id, o => o.MapFrom(i => i.Id))
				.ForMember(e => e.IsActive, o => o.MapFrom(i => i.IsActive))
				.ForMember(e => e.Name, o => o.MapFrom(i => i.Name))
				.ForMember(e => e.Options, o => o.MapFrom(i => i.Options))
				.ForMember(e => e.Photos, o => o.MapFrom(i => i.Photos))
				.ForMember(e => e.Variants, o => o.MapFrom(i => i.Variants));

			CreateMap<Internal.StoreModule.Responses.ProductFindResponse, ProductFindResponse>()
				.ForMember(e => e.Items, o => o.MapFrom(i => i.Items))
				.ForMember(e => e.TotalItems, o => o.MapFrom(i => i.TotalItems))
				.ForMember(e => e.TotalPages, o => o.MapFrom(i => i.TotalPages));

		}
	}
}
