using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace sqlite3jp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Please set DB file and SQL file as argument.");
                    return;
                }

                var dbFilePath = args[0];
                if (!File.Exists(dbFilePath))
                {
                    Console.WriteLine("The specified DB file can not be found.");
                    return;
                }
                dbFilePath.Trim('"');

                var sqlFilePath = args[1];
                if (!File.Exists(sqlFilePath))
                {
                    Console.WriteLine("The specified SQL file can not be found.");
                    return;
                }
                sqlFilePath.Trim('"');

                var sql = "";
                using (var sr = new StreamReader(sqlFilePath, Encoding.GetEncoding("UTF-8")))
                {
                    sql = sr.ReadToEnd();
                }

                var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = dbFilePath };

                using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
                {
                    cn.Open();

                    using (var cmd = new SQLiteCommand(cn))
                    {
                        cmd.CommandText = sql;
                        Console.WriteLine(cmd.ExecuteScalar());
                    }
                }

                Console.WriteLine("SQL execution ended normally.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
