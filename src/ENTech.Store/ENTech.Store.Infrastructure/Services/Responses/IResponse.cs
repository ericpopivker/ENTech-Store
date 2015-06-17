namespace ENTech.Store.Infrastructure.Services.Responses
{
	public interface IResponse
	{
		bool IsSuccess { get; set; }
		
		ResponseError Error { get; set; }

		ArgumentErrorsCollection ArgumentErrors { get; set; }
	}
}