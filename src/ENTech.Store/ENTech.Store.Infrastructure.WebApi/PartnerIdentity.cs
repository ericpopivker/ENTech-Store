using System.Security.Principal;

namespace ENTech.Store.Infrastructure.WebApi
{
	public class PartnerIdentity : IIdentity
	{
		public string Name { get; internal set; }
		public string AuthenticationType { get; internal set; }
		public bool IsAuthenticated { get; internal set; }
		public int PartnerId { get; internal set; }
	}
}