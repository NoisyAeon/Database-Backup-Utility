using Database_Backup_Utility.FileLogger;
using Database_Backup_Utility.src.Database_Types;
using Devart.Data.PostgreSql;


namespace Database_Backup_Utility.src.DBMS
{
    public class PostgreSQLDatabase : Database<PgSqlConnection>
    {
        public override PgSqlConnection Connection { get; protected set; }
        public PostgreSQLDatabase(string server, string name, string user, string password) : base(server, name, user,
            password)
        {
            Connection = new PgSqlConnection(ConnectionString);
            Connection.Open();
            Log.Debug($"Successfully opened connection to database {Name} from PostgreSQL Server at {Server}");

        }

        public override void Dispose()
        {
            Connection.Close();
            Log.Debug($"Closed connection to database {Name}");

        }

        protected override void CreateBackupFile(string path)
        {
            var pgSqlDump = new PgSqlDump();
            pgSqlDump.Connection = Connection;
            pgSqlDump.Backup(path);
        }
    }
}
