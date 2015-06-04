namespace ENTech.Store.Services.Misc
{
	public interface ISecurityInformation
	{
	}
	
	public class AnonymousSecurityInformation : ISecurityInformation
	{
	}


	public class BusinessSecurityInformation : ISecurityInformation
	{
		public int? BusinessId { get; set; }
	}

	//temp fix according to https://entech.kilnhg.com/Code/Eventgrid/Code/QA_api_cleanup/History/839ad5ca5610
	public class BusinessAdminSecurityInformation : BusinessSecurityInformation
	{
	}
}