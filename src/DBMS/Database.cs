using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Backup_Utility.FileLogger;

namespace Database_Backup_Utility.src.Database_Types
{
    public abstract class Database<T>(string server, string name, string user, string password) : IDisposable
    {
        public string Name { get; private set; } = name;
        public string Server { get; private set; } = server;
        public string User { get; private set; } = user;
        private readonly string _password = password;
        public abstract T Connection { get; protected set; }
        protected string ConnectionString
        {
            get { return $"Server={Server};Database={Name};User Id={User};Password={_password};"; }
        }

        public abstract void Dispose();

        protected abstract void CreateBackupFile(string path);
        public void BackupCompressedDatabase(string path = "")
        {
            if (path == "")
                path = Path.Combine(Path.GetTempPath(),$"{Name}_backup.sql");
            Log.Info("Started backup ...");
            Log.Debug($"Started backing up database {Name}");

            var stopwatch = Stopwatch.StartNew();
            CreateBackupFile(path);
            stopwatch.Stop();

            var size = GetFileSize(path);
            Log.ToConsole(LogLevel.Info, $"Created Backup file at {path}. Total Duration: {stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}s File Size: {size.Item1:N2} {size.Item2}");
            Log.Debug($"Successfully created a backup of database {Name}");

        }

        private (double, string) GetFileSize(string path)
        {
            if (!File.Exists(path))
                return (0, "B");

            double fileSize = new FileInfo(path).Length;
            string suffix = "B";

            if (fileSize > 1024)
            {
                fileSize /= 1024;
                suffix = "KB";
            }
            if (fileSize > 1024)
            {
                fileSize /= 1024;
                suffix = "MB";
            }
            if (fileSize > 1024)
            {
                fileSize /= 1024;
                suffix = "GB";
            }

            return (fileSize, suffix);
        }

    }
}
