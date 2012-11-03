using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace PilotPlayer
{
    class MediaObject
    {
        private bool isPaused;
        private double currentVolume;
        private MediaElement videoElementToControl;
        private Image imageElementToControl;

        public MediaObject(MediaElement videoElementToControl)
        {
            this.videoElementToControl = videoElementToControl;
            imageElementToControl = null;
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
