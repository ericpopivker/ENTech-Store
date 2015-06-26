using System.IO;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.UnitTests.StoreModule.Builders
{
	class StoreGetByIdResponseBuilder : BuilderBase<StoreGetByIdResponse, StoreGetByIdResponseBuilder>
	{
		private int _maxProducts;

		public StoreGetByIdResponseBuilder()
		{
			_maxProducts = 10;
		}

		public StoreGetByIdResponseBuilder WithMaxProducts(int value)
		{
			_maxProducts = value;

			return this;
		}

		public override StoreGetByIdResponse Build()
		{
			var response = new StoreGetByIdResponse
			{
				Item = new StoreDto()
				{
					Settings = new StoreSettingsDto
					{
						MaxProducts = _maxProducts
					}
				}
		
			};

			return response;

		}
	}
}
