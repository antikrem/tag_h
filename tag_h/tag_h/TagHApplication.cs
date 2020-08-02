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

        // Main Window reference
        public MainWindow MainWindow = null;

        // Store of all images
        public ImageDatabase ImageDataBase { get; } = null;

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

            // Get all images
            updateHImageQueue();

            this.TagStructure = new TagStructure("tags.xml");
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
            this.ImageDataBase.Close();
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
            this.imageList = this.ImageDataBase.getHImageList();
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
                ImageDataBase.markImageAsViewed(image);
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
            return TagStructure.getRoots();
        }

        // Move tags in tag structure to current image
        public void PushTagStructureToImage()
        {
            MainWindow.CurrentImage.Tags = TagStructure.GetTagString();
        }
    }


}
