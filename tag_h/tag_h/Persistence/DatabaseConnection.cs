﻿using System;
using System.Data.SQLite;

namespace tag_h.Persistence
{
    internal class DatabaseConnection : IDisposable
    {

        private readonly SQLiteConnection _connection;

        public DatabaseConnection()
        {
            _connection = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True;");
            _connection.Open();

            CreateIfNotExistent();
        }

        private readonly string[] _initialiserScripts = new string[]
        {
            @"CREATE TABLE if not exists Images (
                    id INTEGER PRIMARY KEY ASC,
                    fileName STRING NOT NULL, 
                    tags STRING, 
                    viewed INTEGER,
                    deleted INTEGER
                );",
            @"CREATE TABLE if not exists Tags (
                    tag STRING NOT NULL PRIMARY KEY
                );"
        };


        private void CreateIfNotExistent()
        {
            foreach (string query in _initialiserScripts)
            {
                using (var command = CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
                
        }

        public SQLiteCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
