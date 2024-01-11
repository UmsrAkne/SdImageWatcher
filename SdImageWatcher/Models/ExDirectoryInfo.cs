using System.IO;

namespace SdImageWatcher.Models
{
    public class ExDirectoryInfo
    {
        public ExDirectoryInfo()
        {
        }

        public ExDirectoryInfo(string directoryPath)
        {
            var info = new DirectoryInfo(directoryPath);
            FullName = info.FullName;
            Name = info.Name;
        }

        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}