using System.Collections.Generic;
using System.IO;

namespace SdImageWatcher.Models
{
    public class FileUtils
    {
        public static List<string> GetAllFilePaths(string directoryPath)
        {
            var filePaths = new List<string>();

            // 指定されたディレクトリ内のすべてのファイルを取得
            var files = Directory.GetFiles(directoryPath);

            filePaths.AddRange(files);

            // サブディレクトリがある場合、再帰的にファイルを取得
            var subDirectories = Directory.GetDirectories(directoryPath);
            foreach (var subDirectory in subDirectories)
            {
                filePaths.AddRange(GetAllFilePaths(subDirectory));
            }

            return filePaths;
        }
    }
}