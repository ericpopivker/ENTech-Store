namespace ENTech.Store.Services.Internal.SharedModule
{
	public interface ITokenService
	{
		TokenData Create(object associatedObject);
		T GetByToken<T>(string token);
	}
}