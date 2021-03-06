﻿using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class DeleteImageQuery : IQuery
    {
        private HImage _image;

        public DeleteImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET deleted = 1
                        WHERE id = @id;";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.ExecuteNonQuery();
        }
    }

}
