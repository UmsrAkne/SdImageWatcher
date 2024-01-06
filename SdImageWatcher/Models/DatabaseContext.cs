using System.Data.SQLite;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SdImageWatcher.Models
{
    public class DatabaseContext : DbContext
    {
        private const string DbFileName = "db.sqlite";

        public DbSet<ExFileInfo> WatchingDirectoryPaths { get; set; }

        public DbSet<ExFileInfo> Files { get; set; }

        /// <summary>
        ///     指定した ExFileInfo をデータベースに登録します。
        ///     ただし、既存のアイテムと FullName が重複している場合は追加されません。
        /// </summary>
        /// <param name="fileInfo">Files に登録する fileInfo</param>
        public void Add(ExFileInfo fileInfo)
        {
            if (!Files.Any(f => f.FullName == fileInfo.FullName))
            {
                Files.Add(fileInfo);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(DbFileName))
            {
                SQLiteConnection.CreateFile(DbFileName); // ファイルが存在している場合は問答無用で上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = DbFileName, }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}