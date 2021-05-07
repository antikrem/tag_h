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
using System.Windows.Shapes;

namespace tag_h.Views
{
    /// <summary>
    /// Interaction logic for TagAddWindow.xaml
    /// </summary>
    public partial class TagAddWindow : Window
    {
        public string TagName { get; set; } = "";

        public TagAddWindow()
        {
            InitializeComponent();
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TagName = TagNameBox.Text;
            this.Close();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
