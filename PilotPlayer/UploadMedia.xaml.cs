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
using System.IO;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace PilotPlayer
{
    /// <summary>
    /// Interaction logic for UploadMedia.xaml
    /// </summary>
    public partial class UploadMedia : Window
    {
        Timer timer = new Timer();
        string[] mediaURLs;

        DataInterface dbInterface;

        public UploadMedia()
        {
            timer.Interval = 5000;
            InitializeComponent();
            Reset();

            dbInterface = new DataInterface();            
        }

        //ensure the textbox is empty when the program starts and date is set to today
        public void Reset()
        {
            txtUploadPath.Text = String.Empty;
            dtPickerStart.Text = DateTime.Today.ToString();
            dtPickerEnd.Text = DateTime.Today.ToString();
            dtPickerStart.DisplayDateStart = DateTime.Now;
            dtPickerEnd.DisplayDateStart = DateTime.Now;
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
            try
            {
                dbInterface.openConnection();

                MediaObject mediaObject = new MediaObject(txtUploadPath.Text, dtPickerStart.SelectedDate.Value, dtPickerEnd.SelectedDate.Value);

                if (dbInterface.insertMedia(mediaObject))
                {
                    timer.Tick += new EventHandler(eraseLblError);
                    lblStatus.Foreground = Brushes.Green;
                    lblStatus.Content += "Successfully uploaded media.\n";
                    check.Visibility = System.Windows.Visibility.Visible;
                    timer.Start();
                }
                else
                {
                    timer.Tick += new EventHandler(eraseLblError);
                    lblStatus.Foreground = Brushes.Red;
                    lblStatus.Content += "There was an error uploading the media.\n";
                    timer.Start();
                }
            }
            catch (UriFormatException ufe)
            {
                lblStatus.Foreground = Brushes.Red;
                lblStatus.Opacity = 1;
                timer.Tick += new EventHandler(eraseLblError);
                lblStatus.Content += "This file path doesn't seem quite right.\n";
                timer.Start();
            }
            catch (InvalidOperationException ioe)
            {
                lblStatus.Foreground = Brushes.Red;
                lblStatus.Opacity = 1;
                timer.Tick += new EventHandler(eraseLblError);
                lblStatus.Content += ioe.Message.Contains("Nullable") ? "Please ensure that you've selected a file and dates.\n" : ioe.Message + "\n";
                timer.Start();
            }

            dbInterface.closeConnection();
        }


        //btn Start Slideshow will start the application.
        private void btnStartSlideshow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainApplication;

            try
            {
                if (dtPickerEnd.SelectedDate >= dtPickerStart.SelectedDate 
                   || string.IsNullOrWhiteSpace(dtPickerStart.ToString()) || string.IsNullOrWhiteSpace(dtPickerEnd.ToString()))
                {
                    //JOHN: Use this ---> getRandomElement(mediaURLs).ToString(); <---To get a random URI from the database
                    //then we can start the slideshow. 
                    mainApplication = new MainWindow();
                    mainApplication.Show();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please enter a possible date range");
                }
            } 
            catch (SqlCeException sqlEx) 
            {
                lblStatus.Foreground = Brushes.Red;
                lblStatus.Opacity = 1;
                timer.Tick += new EventHandler(eraseLblError);
                lblStatus.Content += "Error starting slideshow. Maybe the database is screwed up.\n";
                timer.Start();
            }
        }

        //Connects to Edit SlideShow Window
        private void btnEditSlideshow_Click(object sender, RoutedEventArgs e)
        {
            var editMedia = new EditSlideshow();
            editMedia.Show();
        }

        public void eraseLblError(object sender, EventArgs e)
        {
            lblStatus.Content = "";
            timer.Stop();
        }

        public Object getRandomElement(Object[] array)
        {
            Random r = new Random();
            int element = r.Next(array.Length);
            return array[element];
        }
    }
}
