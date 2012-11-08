using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;

namespace PilotPlayer
{
    /// <summary>
    /// Interaction logic for EditSlideshow.xaml
    /// </summary>
    public partial class EditSlideshow : Window
    {
        public EditSlideshow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //This code is when I was trying to connect the listView (with a datagrid inside of it) to the database. 

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Connect to DataInterface for removeMedia
        }

    }
}
