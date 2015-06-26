using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public abstract class ResponseError : Error
	{
		protected ResponseError(int errorCode) : base(errorCode)
		{
		}
	}
}
