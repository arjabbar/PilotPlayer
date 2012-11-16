﻿using System;
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
        Timer btnStartfader = new Timer();
        Timer btnStartUnfader = new Timer();
        Timer btnEditfader = new Timer();
        Timer btnEditUnfader = new Timer();
        string[] mediaURLs;
        string projectFolder = System.Windows.Forms.Application.StartupPath + "\\..\\..\\";
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
            dtPickerEnd.DisplayDateStart = DateTime.Parse(DateTime.Today.ToShortDateString() + " 23:59:59");
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
                    dbInterface.openConnection();

                    //dbInterface.updateDateRange(dtPickerStart, dtPickerEnd);
                    mainApplication = new MainWindow();
                    mainApplication.Show();
                    mainApplication.StartSlideshow();
                 
                    dbInterface.closeConnection();
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

        private void btnStart_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnStartUnfader.Tick -= new EventHandler(btnStartUnfader_Tick);
            btnStartfader.Tick += new EventHandler(btnStartFader_Tick);
            btnStartfader.Interval = 10;
            btnStartfader.Start();
        }

        private void btnStart_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnStartfader.Tick -= new EventHandler(btnStartFader_Tick);
            btnStartUnfader.Tick += new EventHandler(btnStartUnfader_Tick);
            btnStartUnfader.Interval = 10;
            btnStartUnfader.Start();
        }

        private void btnStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnStart.Opacity = 0;
            btnStartHover.Opacity = 0;
            btnStartSlideshow_Click(null, new RoutedEventArgs());
        }

        private void btnStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnStartHover.Opacity = 1;
        }

        private void btnStartFader_Tick(Object sender, EventArgs e)
        {
            btnStartUnfader.Stop();
            if (btnStart.Opacity > 0)
            {
                btnStart.Opacity -= 0.1;
            }
            else
            {
                btnStartfader.Stop();
            }
        }

        private void btnStartUnfader_Tick(Object sender, EventArgs e)
        {
            btnStartfader.Stop();
            if (btnStart.Opacity < 1)
            {
                btnStart.Opacity += 0.1;
            }
            else
            {
                btnStartUnfader.Stop();
            }
        }

        private void btnEdit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnEditUnfader.Tick -= new EventHandler(btnEditUnfader_Tick);
            btnEditfader.Tick += new EventHandler(btnEditFader_Tick);
            btnEditfader.Interval = 10;
            btnEditfader.Start();
        }

        private void btnEdit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnEditfader.Tick -= new EventHandler(btnEditFader_Tick);
            btnEditUnfader.Tick += new EventHandler(btnEditUnfader_Tick);
            btnEditUnfader.Interval = 10;
            btnEditUnfader.Start();
        }

        private void btnEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnEdit.Opacity = 0;
            btnEditHover.Opacity = 0;
            btnEditSlideshow_Click(null, new RoutedEventArgs());
        }

        private void btnEdit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnEditHover.Opacity = 1;
        }

        private void btnEditFader_Tick(Object sender, EventArgs e)
        {
            btnEditUnfader.Stop();
            if (btnEdit.Opacity > 0)
            {
                btnEdit.Opacity -= 0.1;
            }
            else
            {
                btnEditfader.Stop();
            }
        }

        private void btnEditUnfader_Tick(Object sender, EventArgs e)
        {
            btnEditfader.Stop();
            if (btnEdit.Opacity < 1)
            {
                btnEdit.Opacity += 0.1;
            }
            else
            {
                btnEditUnfader.Stop();
            }
        }
    }
}
