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
using tag_h.Model;

namespace tag_h
{
    /// <summary>
    /// Interaction logic for TagPanel.xaml
    /// </summary>
    public partial class TagPanel : UserControl
    {
        private TagSet _tags;

        public TagPanel(Model.TagSet tags)
        {
            InitializeComponent();
            _tags = tags;

            this.Background = new SolidColorBrush(
                    ColorStyling.getTagPanelColour(1)
                );

            DrawTagDock();

        }
        // Call back for check box press
        public void OnTagBoxCheck(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Call back for check box unpress
        public void OnTagBoxUncheck(object sender, RoutedEventArgs e)
        {
            TagHApplication.Get().MainWindow.UpdateTagDock();
        }

        // Callback for pressing plus button
        public void ExtendableButton_Click(object sender, RoutedEventArgs e)
        {
            DrawTagDock();
        }

        private void DrawTagDock()
        {
            TagSelector.Children.Clear();

            foreach (Tag tag in _tags)
            {
                CheckBox button = new CheckBox
                {
                    Content = tag,
                    IsChecked = true
                };
                TagSelector.Children.Add(button);
            }

        }

    }
}
