using System;
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
            CreationTime = f.CreationTime;
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

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public bool IsChecked { get; set; }

        [Required]
        public bool Favorite { get; set; }

        [Required]
        public int Rating { get; set; }

        [NotMapped]
        public bool IsDirectory { get; set; }

        [NotMapped]
        public FileSystemInfo FileSystemInfo { get; private set; }

        /// <summary>
        ///     このクラスが保持する FileSystemInfo がファイルだった場合に限り、親ディレクトリの名前。
        ///     それ以外の場合は空文字を返します。
        /// </summary>
        /// <value>
        ///     このクラスが保持する FileSystemInfo の親ディレクトリの名前
        /// </value>
        [NotMapped]
        public string ParentDirectoryName
        {
            get
            {
                if (FileSystemInfo is FileInfo info)
                {
                    var d = info.Directory;
                    return d != null ? d.Name : string.Empty;
                }

                var f = new FileInfo(FullName);
                if (f.Exists)
                {
                    return f.Directory?.Name;
                }

                return string.Empty;
            }
        }

        [NotMapped]
        public int Index { get; set; }
    }
}