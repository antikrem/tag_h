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

namespace tag_h
{
    /// <summary>
    /// Interaction logic for TagPanel.xaml
    /// </summary>
    public partial class TagPanel : UserControl
    {
        Field field;

        public TagPanel(Field field)
        {
            InitializeComponent();
            this.field = field;

            FieldLabel.Content = field.Name;

            // Add radio button
            foreach (Tag i in field.Tags)
            {
                CheckBox button = new CheckBox
                {
                    Content = i.Name,
                    IsChecked = i.IsSelected
                };
                button.Checked += OnTagBoxCheck;
                button.Unchecked += OnTagBoxUncheck;
                TagSelector.Children.Add(button);

                if (i.IsSelected)
                {
                    foreach (Field subField in i.Fields)
                    {
                        TagPanelChildren.Children.Add(
                            new TagPanel(subField)
                        );
                    }
                    
                }
            }
        }

        // Call back for check box press
        public void OnTagBoxCheck(object sender, RoutedEventArgs e)
        {
            // Mark the appropiate tag
            field.MarkWithTag(
                    (string)((CheckBox)sender).Content
                );

            // Update iamge tags and redraw tag dock
            TagHApplication.Get().PushTagStructureToImage();
            TagHApplication.Get().MainWindow.UpdateTagDock();
        }

        // Call back for check box unpress
        public void OnTagBoxUncheck(object sender, RoutedEventArgs e)
        {
            // Mark the appropiate tag
            field.UnmarkWithTag(
                    (string)((CheckBox)sender).Content
                );

            // Update iamge tags and redraw tag dock
            TagHApplication.Get().PushTagStructureToImage();
            TagHApplication.Get().MainWindow.UpdateTagDock();
        }
    }
}
