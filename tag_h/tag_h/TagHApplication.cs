using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Store of all images
        ImageDatabase imageDataBase = null;

        // Place in imageList, -1 indicates start before list
        int place = -1;

        // List of all images, not loaded
        List<HImage> imageList = new List<HImage>();

        // Private constructor
        private TagHApplication()
        {
            // Initialise database
            this.imageDataBase = new ImageDatabase();

            // Get all images
            updateHImageQueue();
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
        private void closeDatabase()
        {
            this.imageDataBase.Close();
        }

        // Closes application
        public static void Close()
        {
            System.Windows.Application.Current.Shutdown();
            TagHApplication.Get().closeDatabase();
        }

        // Updates queue of HImages
        public void updateHImageQueue()
        {
            this.imageList = this.imageDataBase.getHImageList();
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
                return null;
            }
        }
    }


}
