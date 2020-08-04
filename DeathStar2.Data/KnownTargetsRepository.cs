using DeathStar2.Data.Contracts;
using DeathStar2.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DeathStar2.Data
{
    public class KnownTargetsRepository : IKnownTargetsRepository
    {
        public string PATH { get; set; }

        public IList<Target> GetKnownTargets()
        {
            List<Target> collection = new List<Target>();
            if (!File.Exists(PATH))
            {
                return null;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = "select * from [known-targets]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();

                DataTable  dt = new DataTable();
                dt.Load(reader); ;
                while (reader.Read())
                {
                    collection.Add(new Target()
                    {
                        CoOrdinates = reader["CoOrdinates"] as string,
                        Name = reader["Name"] as string
                    });
                }
            }

            return collection;
        }
    }
}
