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

        // Potentially, a reverse place is used, and previous images are looked at
        // These will negative, and zero when the current image is new from the queue
        int reversePlace = 0;

        // Store of all images
        ImageDatabase imageDataBase = null;

        // Queue of awaiting HImages to use, all pre cached
        Queue<HImage> imageQueue = new Queue<HImage>();

        // List of old images, all uncached
        List<HImage> previousImageList = new List<HImage>();

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
            bool skip = false;
            while (this.imageQueue.Count < MAXIMUM_LOADED_IMAGE && !skip)
            {
                var image = this.imageDataBase.getHImage(this.place);

                if (image == null)
                {
                    skip = true;
                } else
                {
                    image.loadBitmap();
                    this.imageQueue.Enqueue(image);
                    this.place++;
                }
            }
        }

        // Gets next image in the queue
        public HImage getNextImage()
        {
            if (this.imageQueue.Count > 0)
            {
                updateHImageQueue();
                return this.imageQueue.Dequeue();
            } else
            {
                return null;
            }
        }

        // Adds image to a previous image list
        public void addUsedImage(HImage image)
        {
            image.deloadBitmap();
            previousImageList.Add(image);
        }
    }


}
