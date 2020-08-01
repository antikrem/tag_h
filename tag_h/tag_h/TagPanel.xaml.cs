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
                RadioButton button = new RadioButton();
                button.Content = i.Name;
                TagSelector.Children.Add(button);
            }
        }
    }
}
