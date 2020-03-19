using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using static ColorStyling;

namespace tag_h
{
    /* *
     * Manages window and front end responsiveness
     * Application logic in TagHApplication 
     * */
    public partial class MainWindow : Window
    {

        // Set to true when window is maximised
        private bool isMaximised = false;

        // Dimensions of restored window size
        private double restoredTop = 300;
        private double restoredLeft = 300;
        private double restoredWidth = 800;
        private double restoredHeight = 600;

        // Current image being drawn
        HImage currentImage = null;

        public MainWindow()
        {
            InitializeComponent();

            // Initialise TagHApplication, the "backend"
            TagHApplication.Get();

            // Maximise the window
            maximiseWindow();

            // Set background colour based on global styling settings
            this.Background = new SolidColorBrush(ColorStyling.getBackgroundColour());

            // Set an image
            this.displayNextImageInQueue(null, null);
        }

        // Closes application
        public void closeApplication(object sender, RoutedEventArgs e)
        {
            TagHApplication.Close();
        }

        // Handle restore button (top right, one right) button press
        public void restoreButtonPress(object sender, RoutedEventArgs e)
        {
            if (this.isMaximised)
            {
                restoreWindow();
            }
            else
            {
                maximiseWindow();
            }
        }

        // Maximises window to cover full screen less taskbar
        public void maximiseWindow()
        {
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;

            this.isMaximised = true;
        }

        // Resizes window to be in non-maximised mode
        public void restoreWindow()
        {
            this.Width = this.restoredWidth;
            this.Height = this.restoredHeight;
            this.Left = this.restoredLeft;
            this.Top = this.restoredTop;
            this.WindowState = WindowState.Normal;

            this.isMaximised = false;
        }

        // Sets next image in the queue to be source
        // Does nothing if queue is empty
        public void displayNextImageInQueue(object sender, RoutedEventArgs e)
        {
            currentImage = TagHApplication.Get().getNextImage();
            if (currentImage != null)
            {
                // Set new values
                CenterImage.Source = currentImage.getBitmap();
                CenterImage.Stretch = Stretch.Uniform;
            }
        }
    }

    
}

