using DeathStar2.Data.Contracts;
using DeathStar2.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DeathStar2.Data
{
    public class SuperLaserRepository : ISuperLaserRepository
    {
        public string PATH { get; set; }

        private const decimal  ChargeVal = 10.0M;

        public decimal GetCapacity()
        {
            decimal  powerreservior = 0.0M;
            if (!File.Exists(PATH))
            {
                return powerreservior;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                string sql = "select * from [SuperLaser]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    powerreservior = Convert.ToDecimal(reader[0].ToString());
                }

                return powerreservior;
            }


        }

        public void CreateSuperLaserTable()
        {
            if (!File.Exists(PATH))
            {
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                var cmd = new SQLiteCommand(connection);
                cmd.CommandText = "DROP TABLE IF EXISTS SuperLaser";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE SuperLaser(powerreservior Double )";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO SuperLaser(powerreservior) VALUES(0.0)";
                cmd.ExecuteNonQuery();

            }
        }

        public void SetCapacity(decimal capacity)
        {
            if (!File.Exists(PATH))
            {
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                var cmd = new SQLiteCommand(connection);
                cmd.CommandText = "Update SuperLaser SET powerreservior =  " + Math.Round(capacity, 2);
                cmd.ExecuteNonQuery();

            }
        }

        public void ChargePower()
        {
           
            if (!File.Exists(PATH))
            {
                return ;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                decimal powerreservior = 0.0M;
                string sql = "select * from [SuperLaser]";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    powerreservior = Convert.ToDecimal(reader[0].ToString());
                }

                var newpower =  ChargeVal  + powerreservior;
                var cmd = new SQLiteCommand(connection);
                cmd.CommandText = "Update SuperLaser SET powerreservior =" + newpower;
                cmd.ExecuteNonQuery();

                return ;
            }
        }

        public decimal Fire(Target target)
        {
            if (!File.Exists(PATH))
            {
                return 0.0M;
            }

            using (var connection = new SQLiteConnection($"Data Source={PATH};Version=3;"))
            {
                connection.Open();
                decimal currentcapacity = 0.0M;
               
                //Updating target destroyed to true
                SQLiteCommand command = new SQLiteCommand(connection);
                //command.CommandText = "Update [known-targets] SET isDestroy =true";
                //command.ExecuteNonQuery();

               //Fetching current capacity
                string sql = "select * from [SuperLaser]";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currentcapacity = Convert.ToDecimal(reader[0].ToString());
                }

                //Reducing power reservior by 12.5 %
                var newcapacity = currentcapacity - currentcapacity * 12.5M / 100;


                var cmd = new SQLiteCommand(connection);
                cmd.CommandText = "Update SuperLaser SET powerreservior = " + newcapacity;
                cmd.ExecuteNonQuery();

                //Calculate output 
                decimal poweroutput = 0.5M * (currentcapacity * currentcapacity);
                
                return poweroutput;
            }

        }

    }
}