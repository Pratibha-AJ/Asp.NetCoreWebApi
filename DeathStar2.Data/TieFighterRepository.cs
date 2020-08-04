using DeathStar2.Data.Contracts;
using DeathStar2.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DeathStar2.Data
{
    public class TieFighterRepository : ITieFighterRepository
    {
        public string PATH { get; set; }

        public IList<TieFighter> GetDamagedTieFighters()
        {
            List<TieFighter> collection = new List<TieFighter>();
            if (!File.Exists(PATH))
            {
                return null;
            }
            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = "select * from [tie-fighters]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    collection.Add(new TieFighter()
                    {
                        Code = reader["Code"] as string,
                        IsDamaged = Convert.ToInt32(reader["IsDamaged"]) > 0 ? true : false
                    });
                }
            }

            //for (var i = 0; i < collection.Count; i++)
            //{
            //    for (var j = i; j < collection.Count; j++)
            //    {
            //        if (collection[i].Code.CompareTo(collection[j].Code) > 0)
            //        {
            //            var tmp = collection[j];
            //            collection[j] = collection[i];
            //            collection[i] = tmp;
            //        }
            //    }
            //}


            //for (var i = 0; i < collection.Count; i++)
            //{
            //    if (collection[i].IsDamaged)
            //    {
            //        tmpCollection.Add(collection[i]);
            //    }
            //}
            //collection = tmpCollection;

            //Used Lambda expressionto refractor code
            var tmpCollection = collection.Where(n => n.IsDamaged == true).ToList();
           

            return tmpCollection;
        }

        public TieFighter GetTieFighterByCode(string code)
        {
            //Its single tieFighter, we dont need list 
            // List<TieFighter> collection = new List<TieFighter>();
            TieFighter tieFighter = new TieFighter();
            if (!File.Exists(PATH))
            {
                return null;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = $"select * from [tie-fighters] WHERE Code = '{code}'";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    //collection.Add(new TieFighter()
                    //{
                    //    Code = reader["Code"] as string,
                    //    IsDamaged = Convert.ToInt32(reader["IsDamaged"]) > 0 ? true : false
                    //});

                    tieFighter = new TieFighter()
                    {
                        Code = reader["Code"] as string,
                        IsDamaged = Convert.ToInt32(reader["IsDamaged"]) > 0 ? true : false
                    };
                }
            }

            return tieFighter;
        }
    }
}
