using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace PilotPlayer
{
    /// <summary>
    /// Interaction logic for UploadMedia.xaml
    /// </summary>
    public partial class UploadMedia : Window
    {


        public UploadMedia()
        {
            InitializeComponent();
        }

        //btn Choose File will allow the user to choose a file to upload
        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFile.ShowDialog();

            if (result == true)
            {
                string file = openFile.FileName;
                txtUploadPath.Text = file;
            }

        }

        //btn Upload will upload the media to the database
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {


        }


        //btn Start Slideshow will start the application.
        private void btnStartSlideshow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainApplication;
                
            //Will do some error checking here for date ranges, etc

            mainApplication = new MainWindow();

        }
    }
}
