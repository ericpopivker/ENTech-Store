using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadDetachRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int UploadId { get; set; }
    }
}