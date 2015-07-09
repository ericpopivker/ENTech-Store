using ENTech.Store.Infrastructure.Services.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Responses
{
    public class UploadGetByIdResponse : InternalResponse
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public int UploadedBytes { get; set; }
        public int SizeBytes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        
    }
}