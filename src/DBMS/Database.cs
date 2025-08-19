using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
