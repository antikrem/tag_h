﻿using System.Collections.Generic;

using tag_h.Persistence;
using tag_h.Tasks;
using tag_h.Model;
using tag_h.Views;

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
        public IImageDatabase ImageDataBase { get; } = null;

        private ITaskRunner _taskRunner;

        HImageList imageList = new HImageList();

        // Private constructor
        private TagHApplication()
        {
            // Initialise database
            this.ImageDataBase = new ImageDatabase();

            _taskRunner = new TaskRunner(ImageDataBase);

            _taskRunner.Submit(new SynchronisePersistence());
            _taskRunner.Submit(new DeleteDuplicates());

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
        
        private void End()
        {
            this.ImageDataBase.Dispose();
            _taskRunner.Stop();
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
            this.imageList = this.ImageDataBase.FetchSampleImageQueue(100);
        }

        // Gets next image in the queue
        // Returns null on end
        public HImage getNextImage()
        {
            if (!imageList.AtEnd())
            {
                imageList.MoveForward();
            }
            return imageList.Get();
        }

        // Gets previous image in the queue
        // Returns null on start
        public HImage getPreviousImage()
        {
            if (!imageList.AtStart())
            {
                imageList.MoveBack();
            }
            return imageList.Get();
        }

    }


}
