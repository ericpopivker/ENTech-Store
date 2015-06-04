namespace ENTech.Store.Infrastructure.Services.Responses
{
	public interface IResponse
	{
		bool IsSuccess { get; set; }

		Error Error { get; set; }
		ArgumentErrorsCollection ArgumentErrors { get; set; }
	}
}