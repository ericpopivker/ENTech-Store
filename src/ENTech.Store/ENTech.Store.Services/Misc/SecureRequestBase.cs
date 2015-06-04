using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.Misc
{
	public abstract class SecureRequestBase<TSecurityInformation> : IInternalRequest
		where TSecurityInformation : ISecurityInformation
	{
		public TSecurityInformation SecurityInformation { get; set; }

		public string UserToken { get; set; }

		[Required]
		public string ApiKey { get; set; }	 
	}
}