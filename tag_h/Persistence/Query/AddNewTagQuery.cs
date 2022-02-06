﻿using System.Collections.Generic;
using System.Data.SQLite;
using tag_h.Core.Model;
using tag_h.Persistence;

namespace tag_h.Core.Persistence.Query
{
    public class AddNewTagQuery : IQuery
    {
        private readonly string _name;

        public AddNewTagQuery(string name)
        {
            _name = name;
        }

        public void Execute(SQLiteCommand command)
        {
            
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT OR REPLACE INTO Tags
                        (name)
                        VALUES (@name);";

                    command.Parameters.AddWithValue("@name", _name);
                    command.ExecuteNonQuery();
                }
            );
        }
    }
}