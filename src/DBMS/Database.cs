using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Backup_Utility.src.Database_Types
{
    abstract class Database
    {
        public string Name { get; private set; }
        public string ConnectionString { get; private set; }
        private string Password;

        public abstract void Connect();

    }
}
