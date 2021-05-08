using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using tag_h.Model;
using tag_h.Persistence;

namespace tag_h.Views
{
    public static class ContainerExtensions {

        // Extends a queue by an enumerable
        public static void Extend<T>(this Queue<T> queue, IEnumerable<T> extension)
        {
            foreach (var x in extension)
            {
                queue.Enqueue(x);
            }
        }

        // Extends a stack by an enumerable
        public static void Extend<T>(this Stack<T> stack, IEnumerable<T> extension)
        {
            foreach (var x in extension)
            {
                stack.Push(x);
            }
        }
    }
    


    /* Manages window and front end responsiveness
     * Application logic in TagHApplication 
     */
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
        public HImage CurrentImage = null;

        // Image 100% zoom size
        double imageDefaultWidth = 0;
        double imageDefaultHeight = 0;

        // Offset zoom
        double zoom = 1;

        // Current center focus
        Point centerFocus = new Point(0, 0);

        // Mouse drag start
        Point mouseDragOffset = new Point(0, 0);

        // Specifies that the mouse is currently dragging
        bool mouseIsDragging = false;

        // Used to only drag after sufficient drags are made
        int dragCount = 0;

        // Constant that indicates the number of IFrames before mouse moves
        static int dragIFrames = 5;

        public MainWindow()
        {
            InitializeComponent();

            // Initialise TagHApplication, the "backend"
            TagHApplication.Get().MainWindow = this;

            // Maximise the window
            maximiseWindow();

            // Set background colour based on global styling settings
            this.Background = new SolidColorBrush(ColorStyling.getBackgroundColour());

            // Set an image
            this.MainWindow_DisplayNextImageInQueue(null, null);
        }

        // Closes application
        public void MainWindow_CloseWindow(object sender, RoutedEventArgs e)
        {
            TagHApplication.Close();
        }

        // Handle restore button (top right, one right) button press
        public void MainWindow_RestoreButtonPress(object sender, RoutedEventArgs e)
        {
            if (this.isMaximised)
            {
                restoreWindow();
            }
            else
            {
                maximiseWindow();
            }
            updateCenterImageView();
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

        // Sets center image to show full iamge, and not scale smaller images
        private void updateCenterImageView()
        {
            double imageWidth;
            double imageHeight;

            if (CurrentImage is null)
            {
                return;
            }

            (imageWidth, imageHeight) = CurrentImage.Size;

            if (imageHeight > this.Height)
            {
                imageWidth = (this.Height / imageHeight) * imageWidth;
                imageHeight = this.Height;

            }

            if (imageWidth > this.Width)
            {
                imageHeight = (this.Width / imageWidth) * imageHeight;
                imageWidth = this.Width;
                
            }

            CenterImage.Width = imageWidth;
            CenterImage.Height = imageHeight;
            imageDefaultWidth = imageWidth;
            imageDefaultHeight = imageHeight;

            // zoom and center
            zoomImage(1);
            centerImage();
        }

        // Set new center image
        // Frees old image
        private void setNewImage(HImage nextImage)
        {
            // If there is no next image, do nothing
            if (nextImage is null)
            {
                return;
            }
            
            // If theres a current image, save tags
            if (CurrentImage != null)
            {
                TagHApplication.Get().ImageDataBase.SaveImage(CurrentImage);
            }

            // New image is the current image
            CurrentImage = nextImage;

            // Set new image
            CenterImage.Source = CurrentImage.getBitmap();

            // Set CenterImage to be correct size
            updateCenterImageView();

            // Also center the image
            centerImage();

            // Draw Tag Dock
            this.UpdateTagDock();
        }

        // Sets next image in the queue to be source
        // Does nothing if queue is empty
        public void MainWindow_DisplayNextImageInQueue(object sender, RoutedEventArgs e)
        {
            setNewImage(TagHApplication.Get().getNextImage());

        }

        // Sets last image in the queue to be source
        // Does nothing if queue is empty
        public void MainWindow_DisplayPreviousImageInQueue(object sender, RoutedEventArgs e)
        {
            setNewImage(TagHApplication.Get().getPreviousImage());

        }

        // Centers the image, such that the given pixel of the image
        // At the current resolution will be in the center of the image
        public void centerImageAt(double x, double y)
        {
            // Centering image by displacement of size
            CenterImage.Margin = new Thickness(
                (this.Width / 2) - this.zoom * x,
                (this.Height / 2)  - this.zoom * y, 
                0, 
                0
            );
            centerFocus = new Point(x, y);
        }

        // More generalise image center
        // Centers at the direct middle of image
        public void centerImage()
        {
            centerImageAt(
                imageDefaultWidth / 2,
                imageDefaultHeight / 2
            );
        }

        // Zooms image to given zoomOffset
        public void zoomImage(double zoom)
        {
            this.zoom = zoom;
            CenterImage.Width = this.zoom * imageDefaultWidth;
            CenterImage.Height = this.zoom * imageDefaultHeight;
        }


        // Handle for image mouse down, which could be a number of events
        public void MainWindow_CenterImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Handle a double click
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2) {
                // Choose zoom depending if its already zoomed
                if (zoom == 1)
                {
                    zoomImage(2);
                    // Center image at mouse click position
                    Point clickPoint = e.GetPosition(CenterImage);
                    centerImageAt(clickPoint.X, clickPoint.Y);
                }
                else
                {
                    zoomImage(1);
                    // Center image right in the middle
                    centerImage();
                }
                
            }

            // set dragging as true at the current mouse drag position
            mouseIsDragging = true;
            dragCount = 0;
            mouseDragOffset = new Point(
                e.GetPosition(CenterImage).X / zoom,
                e.GetPosition(CenterImage).Y / zoom
            );
        }

        // Handle for image mouse up, which resets mouse drag
        public void MainWindow_CenterImageMouseDragStop(object sender, RoutedEventArgs e)
        {
            mouseIsDragging = false;
            dragCount = 0;
        }

        // Handle event of mouse moving over image
        public void MainWindow_CenterImageMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDragging)
            {
                dragCount++;
                if (dragCount > dragIFrames)
                {
                    // Find the change in mouse position and update image
                    var currentMouse = e.GetPosition(CenterImage);
                    double x = mouseDragOffset.X - currentMouse.X / zoom;
                    double y = mouseDragOffset.Y - currentMouse.Y / zoom;

                    centerFocus.X = centerFocus.X + x;
                    centerFocus.Y = centerFocus.Y + y;
                    centerImageAt(centerFocus.X, centerFocus.Y);
                }
               
            }
        }

        // Handle showing Tagdock on bar hover
        public void MainWindow_ShowTagDock(object sender, MouseEventArgs e)
        {
            TagDock.Visibility = Visibility.Visible;
            TagBar.Visibility = Visibility.Hidden;

            // Draw Tag Dock
            this.UpdateTagDock();
        }

        // Updates dock with current tag structure
        public void UpdateTagDock()
        {
            // Do not draw if tag dock is not shown
            if (TagDock.Visibility != Visibility.Visible)
            {
                return;
            }

            TagDock.Children.Clear();

            if (CurrentImage is null)
            {
                return;
            }

            // Update tag structure with tags
            //TagHApplication.Get().TagStructure.MarkWithTags(CurrentImage.Tags);
            //TagHApplication.Get().TagStructure.Roots.ForEach(x => TagDock.Children.Add(new TagPanel(x, 0)));
        }
    }

    
}

