using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Data.SqlServerCe;
using System.Data.SqlClient;

namespace PilotPlayer
{
    class MediaObject
    {
        private bool isPaused;
        private double currentVolume;
        private MediaElement videoElementToControl;
        private Image imageElementToControl;
 
        private string url;
        private DateTime dateStart, dateEnd;

        public MediaObject(MediaElement videoElementToControl)
        {
            this.videoElementToControl = videoElementToControl;
            imageElementToControl = null;
        }

        public MediaObject(string path, DateTime startDate, DateTime endDate)
        {
            if (path == String.Empty)
            {
                throw new System.InvalidOperationException("Please enter a url for your media.");
            }
            else if (startDate == null || endDate == null)
            {
                throw new System.InvalidOperationException("Please enter a start and end date.");
            }

            this.url = path;
            this.dateStart = startDate;
            this.dateEnd = endDate;
        }

        public String getUrl()
        {
            return url;
        }

        public DateTime getStartDate()
        {
            return dateStart;
        }

        public DateTime getEndDate()
        {
            return dateEnd;
        }

        public MediaObject(Image imageElementToControl)
        {
            this.imageElementToControl = imageElementToControl;
            videoElementToControl = null;
        }

        public void muteAudio()
        {
            currentVolume = videoElementToControl.Volume;
            videoElementToControl.Volume = 0;
        }

        public void unMuteAudio()
        {
            videoElementToControl.Volume = currentVolume;
        }

        public void displayMedia()
        {
            try
            {
                playVideoMedia();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void stopVideoMedia()
        {
            videoElementToControl.Stop();
        }

        public void playVideoMedia()
        {
            videoElementToControl.Play();
        }
        
        public void pauseVideoMedia()
        {
            if (videoElementToControl.CanPause && !isPaused)
            {
                videoElementToControl.Pause();
                isPaused = true;
            }
            else
            {
                isPaused = false;
                videoElementToControl.Play();
            }
        }
    }
}
