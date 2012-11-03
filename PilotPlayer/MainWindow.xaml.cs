﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PilotPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaObject mObject;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            Topmost = true;

            //WindowState = WindowState.Maximized;
            primaryMediaElement.LoadedBehavior = MediaState.Manual;
            primaryMediaElement.Clock = null;
            System.Uri mediaURI = new System.Uri("e:/video.wmv");
        
            primaryMediaElement.Source = mediaURI;
            mObject = new MediaObject(primaryMediaElement);
            mObject.playVideoMedia();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            if (e.Key == Key.P)
            {
                mObject.displayMedia();
            }
            if (e.Key == Key.Space)
            {
                mObject.pauseVideoMedia();
            }

        }
    }
}
