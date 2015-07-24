using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.ProductModule.Errors
{
	public class ProductResponseErrorCode : ErrorCodeBase
	{
		public const int OverStoreMaxProductsLimit = 20001;

	}
}
