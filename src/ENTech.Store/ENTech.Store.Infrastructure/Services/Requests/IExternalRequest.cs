namespace ENTech.Store.Infrastructure.Services.Requests
{
	public interface IExternalRequest
	{
		string UserToken { get; set; }

		string PartnerToken { get; set; }
	}
}