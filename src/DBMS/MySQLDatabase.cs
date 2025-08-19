using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Backup_Utility.FileLogger;
using Database_Backup_Utility.src.Database_Types;
using MySql.Data.MySqlClient;

namespace Database_Backup_Utility.src.DBMS
{
    public class MySQLDatabase : Database<MySqlConnection>
    {
        public override MySqlConnection Connection { get; protected set; }

        public MySQLDatabase(string server, string name, string user, string password) : base(server, name, user,
            password)
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            Log.Debug($"Successfully opened connection to database {Name} from MySQL Server at {Server}");

        }


        public override void Dispose()
        {
            Connection.Close();
            Log.Debug($"Closed connection to database {Name}");
        }
    }
}
