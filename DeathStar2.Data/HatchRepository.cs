using DeathStar2.Data.Contracts;
using DeathStar2.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DeathStar2.Data
{
    public class HatchRepository : IHatchRepository
    {
        public string PATH { get; set; }

        public Hatch GetHatch(int id)
        {
            List<Hatch> collection = new List<Hatch>();
            if (!File.Exists(PATH))
            {
                return null;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = "select * from [hatches]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    collection.Add(new Hatch()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        IsOpen = Convert.ToInt32(reader["IsOpen"]) > 0 ? true : false,
                        X = Convert.ToInt32(reader["X"]),
                        Y = Convert.ToInt32(reader["Y"]),
                        Z = Convert.ToInt32(reader["Z"])
                    });
                }
            }

            return collection.SingleOrDefault(c => c.ID == id);
        }

        public IList<Hatch> GetHatches()
        {
            List<Hatch> collection = new List<Hatch>();
            if (!File.Exists(PATH))
            {
                return null;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = "select * from [hatches]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    collection.Add(new Hatch()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        IsOpen = Convert.ToInt32(reader["IsOpen"]) > 0 ? true : false,
                        X = Convert.ToInt32(reader["X"]),
                        Y = Convert.ToInt32(reader["Y"]),
                        Z = Convert.ToInt32(reader["Z"])
                    });
                }
            }

            return collection;
        }
    }
}
