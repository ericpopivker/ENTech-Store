using ENTech.Store.Services.Misc;
using System.IO;

namespace ENTech.Store.Services.UploadModule.Requests
{
    public class UploadGetByIdRequest : SecureRequestBase<BusinessAdminSecurityInformation>
    {
        public int Id { get; set; }
    }
}