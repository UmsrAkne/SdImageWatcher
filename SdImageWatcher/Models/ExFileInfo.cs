using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace SdImageWatcher.Models
{
    public class ExFileInfo
    {
        public ExFileInfo(FileSystemInfo f)
        {
            FileSystemInfo = f;
            Name = f.Name;
            FullName = f.FullName;
            IsDirectory = new DirectoryInfo(f.FullName).Exists;
        }

        public ExFileInfo()
        {
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Index(IsUnique = true)]
        public string FullName { get; set; } = string.Empty;

        [NotMapped]
        public bool IsDirectory { get; set; }

        [NotMapped]
        public FileSystemInfo FileSystemInfo { get; private set; }
    }
}