using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Persistence
{
    class ImageDatabase : IDisposable
    {

        private ImageDatabaseConnection _connection;

        public ImageDatabase()
        {
            _connection = new ImageDatabaseConnection();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void AddNewImage(string fileName)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"INSERT INTO Images (fileName, tags, viewed) 
                        VALUES (@fileName, NULL, 0);";
                command.Parameters.AddWithValue("@fileName", fileName);
                command.ExecuteNonQuery();
            }
        }

        public void Hydrate(HImage image)
        {

        }

        public void SaveImage(HImage image)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"UPDATE Images
                        SET tags = @tags
                        
                        id = @id;";
                command.Parameters.AddWithValue("@id", image.UUID);
                command.ExecuteNonQuery();
            }
        }

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE SampleTable (Col1 VARCHAR(20), Col2 INT)";
            string Createsql1 = "CREATE TABLE SampleTable1 (Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();

        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1 (Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }

    }
}