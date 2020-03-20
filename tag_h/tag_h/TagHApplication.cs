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

        // Place in queue
        int place = 0;
        // Store of all images
        ImageDatabase imageDataBase = null;

        // Queue of awaiting HImages to use
        Queue<HImage> imageQueue = new Queue<HImage>();

        // Private constructor
        private TagHApplication()
        {
            // Initialise database
            this.imageDataBase = new ImageDatabase();
            updateHImageQueue();
        }

        // Singleton accessor
        public static TagHApplication Get()
        {
            if (instance == null)
            {
                instance = new TagHApplication();
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
            bool skip = false;
            while (this.imageQueue.Count < MAXIMUM_LOADED_IMAGE && !skip)
            {
                var image = this.imageDataBase.getHImage(place);

                if (image == null)
                {
                    skip = true;
                } else
                {
                    image.loadBitmap();
                    imageQueue.Enqueue(image);
                    place++;
                }
            }
        }

        // Gets next iamge in the queue
        public HImage getNextImage()
        {
            if (imageQueue.Count > 0)
            {
                updateHImageQueue();
                return imageQueue.Dequeue();
            } else
            {
                return null;
            }
        }
    }


}
