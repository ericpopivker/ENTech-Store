using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.SharedModule.Requests
{
    public class EntityFinishUploadRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int UploadId { get; set; }
        public int EntityId { get; set; }
        public string EntityFieldName { get; set; }
        public string CdnUrl { get; set; }
    }
}
