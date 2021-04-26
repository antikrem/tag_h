﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tag_h.Persistence;
using tag_h.Tasks;

namespace tag_h
{
    // Represents the internal state of the application
    // Uses a singleton pattern
    class TagHApplication
    {
        // Maximum number of HImages in queue
        private const int MAXIMUM_LOADED_IMAGE = 8;

        // Singleton isntance
        private static TagHApplication instance = null;

        // Main Window reference
        public MainWindow MainWindow = null;

        // Store of all images
        public ImageDatabase ImageDataBase { get; } = null;

        private TaskRunner _taskRunner;

        // Place in imageList, -1 indicates start before list
        int place = -1;

        // List of all images, not loaded
        List<HImage> imageList = new List<HImage>();

        // Current tag structure used by application
        public TagStructure TagStructure { get; set; } = null;

        // Private constructor
        private TagHApplication()
        {
            // Initialise database
            this.ImageDataBase = new ImageDatabase();

            _taskRunner = new TaskRunner(ImageDataBase);

            _taskRunner.Submit(new SynchronisePersistence());

            // Get all images
            UpdateHImageQueue();

        //    this.TagStructure = new TagStructure("tags.xml");
        }

        // Singleton accessor
        public static TagHApplication Get()
        {
            if (TagHApplication.instance == null)
            {
                TagHApplication.instance = new TagHApplication();
            }
            return instance;
        }
        
        // Close the database
        private void End()
        {
            this.ImageDataBase.Dispose();
            _taskRunner.Stop();
            TagStructure.SaveTagStructure();
        }

        // Closes application
        public static void Close()
        {
            TagHApplication.Get().End();
            System.Windows.Application.Current.Shutdown();
        }

        // Updates queue of HImages
        private void UpdateHImageQueue()
        {
            this.imageList = this.ImageDataBase.FetchAllImages();
        }

        // Gets next image in the queue
        // Returns null on end
        public HImage getNextImage()
        {
            place++;
            if (place < imageList.Count)
            {
                var image = imageList[place];
                image.loadBitmap();
                return image;
            } else
            {
                place = imageList.Count - 1;
                return null;
            }
        }

        // Gets previous image in the queue
        // Returns null on start
        public HImage getPreviousImage()
        {
            place--;
            if (place >= 0)
            {
                var image = imageList[place];
                image.loadBitmap();
                return image;
            }
            else
            {
                place = 0;
                return null;
            }
        }

        // Gets root field of tag structure
        public List<Field> getRootFields()
        {
            return TagStructure.Roots;
        }

        // Move tags in tag structure to current image
        public void PushTagStructureToImage()
        {
            MainWindow.CurrentImage.Tags = TagStructure.GetTagString();
        }
    }


}
