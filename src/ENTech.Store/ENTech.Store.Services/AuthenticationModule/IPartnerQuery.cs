namespace ENTech.Store.Services.AuthenticationModule
{
	public interface IPartnerQuery
	{
		int? GetByApiKey(string apiKey);
	}
}