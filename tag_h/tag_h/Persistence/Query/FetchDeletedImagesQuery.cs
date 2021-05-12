﻿using System.Collections.Generic;
using System.Data.SQLite;
using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchDeletedImagesQuery : IQuery
    {
        private readonly IHImageRepository _imageRepository;

        public HImageList Result { get; private set; }

        public FetchDeletedImagesQuery(IHImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void Execute(SQLiteCommand command)
        {
            List<HImage> images = new List<HImage>();

            command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 1;";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                images.Add(dataReader.GetHImage());
            }

            Result = new HImageList(_imageRepository, images);
        }
    }

}
