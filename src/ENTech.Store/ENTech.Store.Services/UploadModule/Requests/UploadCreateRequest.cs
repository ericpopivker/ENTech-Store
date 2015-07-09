using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadCreateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public string Role { get; set; }

        public UploadCreateRequest()
        {
        }

        public UploadCreateRequest(string role)
        {
            this.Role = role;
        }
         
    }
}