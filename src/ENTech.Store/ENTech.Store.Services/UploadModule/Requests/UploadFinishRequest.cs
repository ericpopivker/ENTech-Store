using ENTech.Store.Services.Misc;
using System.IO;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadFinishRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int UploadId { get; set; }
        public string AttachedEntityType { get; set; }
        public int AttachedEntityId { get; set; }
        public string AttachedEntityFieldName { get; set; }
    }
}