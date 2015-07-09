using ENTech.Store.Services.Misc;
using System.IO;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadAttachRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int UploadId { get; set; }
        public string AttachingEntityType { get; set; }
        public string AttachingEntityFieldName { get; set; }
        public int AttachingEntityId { get; set; }
    }
}