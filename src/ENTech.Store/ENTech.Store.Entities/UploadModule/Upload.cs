using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.UploadModule
{
    [Table("Upload")]
    public class Upload : IEntity, IAuditable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public string FileExtension { get; set; }
        public int SizeBytes { get; set; }
        public int UploadedBytes { get; set; }

        public string AttachedEntityType { get; set; }
        public int AttachedEntityId { get; set; }
        public string AttachedEntityFieldName { get; set; }

        public bool IsProcessed { get; set; }
        public bool IsAttached { get; set; }
        public bool IsUploaded { get; set; }
        public string CdnUrl { get; set; }
    }
}
