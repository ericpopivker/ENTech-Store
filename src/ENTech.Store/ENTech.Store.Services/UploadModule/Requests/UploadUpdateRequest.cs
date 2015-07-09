using ENTech.Store.Services.Misc;
using System.IO;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadUpdateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int Id { get; set; }
        public Stream Stream { get; set; }
        public string Extension { get; set; }
    }
}