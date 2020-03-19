using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tag_h
{
    /*
     * Store for all images that are used by this application
     * Minimises memory use by keeping some images only as references
     * to files on HDD
     */
    class ImageDatabase
    {
        // All currently supported file types
        private static readonly string[] SupportedFileExtensions = {
        ".png",
        ".jpg"
        };

        private List<string> files = new List<string>();

        // Location of all images to be used
        private Uri location;

        // Creates ImageDatabase relative to a given folder
        public ImageDatabase(string location)
        {
            // Initialise database
            this.location = new Uri(location);

            // Get all filepaths
            string[] filePaths = Directory.GetFiles(location, "*", SearchOption.TopDirectoryOnly);

            // Only accept images with supported file end
            foreach (string i in filePaths)
            {
                if (SupportedFileExtensions.Any(x => i.EndsWith(x)))
                {
                    files.Add(i);
                }
            }
        }

        // Get a HImage with a condition and offset
        public HImage getHImage(int offset)
        {
            return new HImage(files[offset]);
        }

    }
}
