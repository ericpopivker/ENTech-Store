using System;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;

namespace ENTech.Store.Services.UnitTests.ProductModule.Builders
{
	class ProductCreateRequestBuilder : BuilderBase<ProductCreateRequest, ProductCreateRequestBuilder>
	{
		private string _name;
	
		public ProductCreateRequestBuilder()
		{
			_name = "ProductName_" + Guid.NewGuid();
		}

		public ProductCreateRequestBuilder WithName(string value)
		{
			_name = value;

			return this;
		}

		public override ProductCreateRequest Build()
		{
			var request = new ProductCreateRequest
			{
				Product = new ProductCreateDto
				{
					Name = _name
				}
			};

			return request;

		}
	}
}
