using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace tag_h
{
    /*
     * Store for all images that are used by this application
     * Minimises memory use by keeping some images only as references
     * to files on HDD
     */
    class ImageDatabase
    {
        // working directory of database
        private static string workingDirectory;

        // Database connect string to local database storing tags
        private string dbConnectionString 
                = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbTags.mdf;Integrated Security = True";

        // Local file storing all images
        private static readonly string imageFolder = "images//";

        // All currently supported file types
        private static readonly string[] SupportedFileExtensions = {
        ".png",
        ".jpg"
        };

        // connection to database used to store tags
        private SqlConnection dbConnection = null;

        // Sets up necessary folders for execution
        private static void setupFolders()
        {
            // Create image folder
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
        }

        private List<string> files = new List<string>();

        // Checks if image exists in database
        private bool checkImageInDatabase(string fileName)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Tags WHERE FileName=@fileName", dbConnection);
            cmd.Parameters.AddWithValue("@fileName", fileName);

            using (SqlDataReader objReader = cmd.ExecuteReader())
            {
                // If there are tables, implies there is a record with that filename
                return objReader.Read();
            }
        }

        // Mark all images as non-physical existent
        // Used to delete non-existent images
        private void markAllImagesAsNonExistent()
        {
            SqlCommand cmd = new SqlCommand("UPDATE dbo.Tags SET PhysicalExists = 0;", dbConnection);

            cmd.ExecuteNonQuery();
        }

        // Add a new file to database
        private void addNewImage(string fileName)
        {
            string commandLine =
                "INSERT INTO dbo.Tags (FileName, Tags, PhysicalExists, New, Viewed) " +
                "VALUES (@fileName, NULL, 1, 1, 0);"; 
            SqlCommand cmd = new SqlCommand(commandLine, dbConnection);
            cmd.Parameters.AddWithValue("@fileName", fileName);

            cmd.ExecuteNonQuery();
        }

        // Mark an existing image as physically existent
        // Also sets these images as old
        private void markImagePhysicallyExistent(string fileName)
        {
            string commandLine =
                "UPDATE dbo.Tags SET PhysicalExists = 1, New = 0 " +
                "WHERE FileName = @fileName;";
            SqlCommand cmd = new SqlCommand(commandLine, dbConnection);
            cmd.Parameters.AddWithValue("@fileName", fileName);

            cmd.ExecuteNonQuery();
        }

        // Deletes all images marked non-physical existent, returns number deleted
        private int deletePhysicallyNonExistent()
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Tags WHERE PhysicalExists = 0;", dbConnection);

            return cmd.ExecuteNonQuery();
        }

        // Add all new images to database, and marks existing images as existent
        // Returns number of new files added
        private int pushPhysicalToDatabase()
        {
            // Get all filepaths
            string[] filePaths = Directory.GetFiles(imageFolder, "*", SearchOption.TopDirectoryOnly);

            int count = 0;
            // Only accept images with supported file end
            foreach (string i in filePaths)
            {
                // Only consider files with supported file ends 
                if (SupportedFileExtensions.Any(x => i.EndsWith(x)))
                {
                    if (checkImageInDatabase(i))
                    {
                        // Mark image as physically existing
                        markImagePhysicallyExistent(i);
                    } else
                    {
                        // Add this image to the database
                        addNewImage(i);
                        count++;
                    }
                    
                }
            }
            return count;
        }

        // Updates all database values to reflect whats on disk
        private void updateDatabase()
        {
            // Mark all existing images as non-existent
            markAllImagesAsNonExistent();

            // Add all new images to database
            int newImageCount = pushPhysicalToDatabase();

            // Delete non existent images
            int deletedImageCount = deletePhysicallyNonExistent();

            Console.WriteLine("Added " + newImageCount + " new images");
            Console.WriteLine("Deleted " + deletedImageCount + " new images");
        }

        // Creates ImageDatabase relative to a given folder
        public ImageDatabase()
        {
            // Set current working directory method
            ImageDatabase.workingDirectory = Directory.GetCurrentDirectory();

            // Sets up the image directory if it does not already exist
            setupFolders();

            // Initialise database
            this.dbConnection = new SqlConnection(dbConnectionString);
            this.dbConnection.Open();

            // Get all filepaths
            string[] filePaths = Directory.GetFiles(imageFolder, "*", SearchOption.TopDirectoryOnly);

            // Only accept images with supported file end
            foreach (string i in filePaths)
            {
                if (SupportedFileExtensions.Any(x => i.EndsWith(x)))
                {
                    files.Add(ImageDatabase.workingDirectory + "//" + i);
                }
            }

            // Update the database from disk
            updateDatabase();
        }

        // Closes database
        public void Close()
        {
            this.dbConnection.Close();
        }

        // Marks an image as viewed
        public void markImageAsViewed(HImage image)
        {
            string commandLine =
                "UPDATE dbo.Tags SET Viewed = 1, New = 0 " +
                "WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(commandLine, dbConnection);
            cmd.Parameters.AddWithValue("@id", image.UUID);

            cmd.ExecuteNonQuery();
        }

        // Loads HImage tags from database
        public void LoadHTags(HImage image)
        {
            string commandLine =
                "SELECT Tags FROM dbo.Tags " +
                "WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(commandLine, dbConnection);
            cmd.Parameters.AddWithValue("@id", image.UUID);

            using (SqlDataReader objReader = cmd.ExecuteReader())
            {
                if (objReader.Read() && !objReader.IsDBNull(0))
                {
                    var tagInDatabase = objReader.GetString(0);
                    image.Tags = objReader.GetString(0).Split(',').ToList();
                }
            }
        }

        // Saves HImage back to database
        public void SaveHTags(HImage image)
        {
            string commandLine =
                "UPDATE dbo.Tags SET Tags = @tags " +
                "WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(commandLine, dbConnection);
            cmd.Parameters.AddWithValue("@id", string.Join(",", image.UUID));
            cmd.Parameters.AddWithValue("@tags", string.Join(",", image.Tags));

            cmd.ExecuteNonQuery();
        }

        // Get a HImage with a condition and offset
        // Returns null if offset is too bit
        public List<HImage> getHImageList()
        {
            List<HImage> hImages = new List<HImage>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Tags", dbConnection);

            using (SqlDataReader objReader = cmd.ExecuteReader())
            {
                while (objReader.Read())
                {
                    hImages.Add(new HImage(objReader.GetInt32(0), objReader.GetString(1)));
                }
            }

            // Load Tags for each hImage
            foreach (var image in hImages)
            {
                this.LoadHTags(image);
            }

            return hImages;

        }

        // Gets the image folder location
        public static string getImageFolderLocation()
        {
            return imageFolder;
        }

    }
}
