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
        private string insertQuery;
        private string url, fileName, fileExt, fileType;
        private int typeID, width, height, length;
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
                this.fileName = path.Split('\\').Last();
                this.fileExt = fileName.Split('.').Last();
                this.fileName = fileName.Split('.').First();
                this.typeID = MediaFileUtilities.getFileTypeID(this.fileExt);
                this.fileType = MediaFileUtilities.getFileType(this.fileExt);
                MediaElement thisMediaFile = new MediaElement();
                thisMediaFile.Source = new Uri(path);
                this.width = (int)thisMediaFile.Width;
                this.height = (int)thisMediaFile.Height;
                this.dateStart = startDate;
                this.dateEnd = endDate;
            
            this.insertQuery = "INSERT INTO Media([url],[filename],[file_extension],[type_id],[width],[height],[date_start],[date_end])"
                    + "VALUES ('" + url + "','" + fileName + "','" + fileExt + "','" + typeID + "','" + width + "','"
                    + height + "','" + dateStart + "','" + dateEnd + "');";
            
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

        public bool insertMediaFile(SqlCeConnection sc)
        {
            if (String.IsNullOrWhiteSpace(insertQuery))
            {
                return false;
            }
            Console.WriteLine(sc.ConnectionString);
            sc.Open();
            SqlCeDataReader sqlRdr;
            try
            {
                SqlCeCommand sqlCmd = new SqlCeCommand(insertQuery, sc);
                sqlRdr = sqlCmd.ExecuteReader();
                sqlRdr.Close();
                sc.Close();
                return true;
            }
            catch (SqlCeException sqlEx)
            {
                return false;
            }
        }
    }
}
