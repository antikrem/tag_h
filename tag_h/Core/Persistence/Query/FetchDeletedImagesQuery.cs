﻿using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchDeletedImagesQuery : IQuery
    {

        public List<HImage> Result { get; private set; }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            List<HImage> images = new();

            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 1;";

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        images.Add(dataReader.GetHImage());
                    }
                }
            );

            Result = images;
        }
    }

}
