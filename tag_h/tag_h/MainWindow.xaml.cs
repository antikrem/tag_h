﻿using System;
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
            double imageWidth = currentImage.getPixelWidth();
            double imageHeight = currentImage.getPixelHeight();

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
            zoomImage(1);
        }

        // Sets next image in the queue to be source
        // Does nothing if queue is empty
        public void displayNextImageInQueue(object sender, RoutedEventArgs e)
        {
            // Get the next image
            var nextImage = TagHApplication.Get().getNextImage();

            // If there is a next image, do something
            if (nextImage != null)
            {
                // If theres a current image, dispose it
                if (currentImage != null)
                {
                    TagHApplication.Get().addUsedImage(currentImage);
                }

                // New image is the current image
                currentImage = nextImage;

                // Set new image
                CenterImage.Source = currentImage.getBitmap();

                // Set CenterImage to be correct size
                updateCenterImageView();

                // Also center the image
                centerImage();
            }

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
        public void centerImageMouseDown(object sender, MouseButtonEventArgs e)
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
        public void centerImageMouseDragStop(object sender, RoutedEventArgs e)
        {
            mouseIsDragging = false;
            dragCount = 0;
        }

        // Handle event of mouse moving over image
        public void centerImageMouseMove(object sender, MouseEventArgs e)
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

    }

    
}

