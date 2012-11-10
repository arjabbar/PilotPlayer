using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Windows.Controls;

/*
 * This Class will act as the interface to our database. 
 * Contains ethods for connecting, disconnecting, inserting, and querying the database.
 */

namespace PilotPlayer
{
    class DataInterface
    {
        SqlCeConnection sc;

        String appPath;

        public DataInterface()
        {
            appPath = Application.StartupPath;
            sc = new SqlCeConnection("Data Source=" + appPath + "\\PilotPlayerDB.sdf" + ";Persist Security Info=False;");
        }

        public SqlCeConnection getSqlConnection()
        {
            return sc;
        }

        //Opens connection to database. Returns true if connection is made successfully, false otherwise.
        public Boolean openConnection()
        {
            try
            {
                sc.Open();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        //Closes connection to database. Returns true if connection is closed successfully, false otherwise.
        public Boolean closeConnection()
        {
            try
            {
                sc.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Inserts an entry into the media table and returns true if insertion is successful, and false otherwise.
        public Boolean insertMedia(MediaObject mObject)
        {
            SqlCeCommand sqlCmd;
            SqlCeDataReader sqlRdr;
            
            string insertQuery;
            string url, fileName, fileExt, fileType;
            int typeID, width, height;
            DateTime dateStart, dateEnd;

            url = mObject.getUrl();
            fileName = url.Split('\\').Last();
            fileExt = fileName.Split('.').Last();
            fileName = fileName.Split('.').First();
            typeID = MediaFileUtilities.getFileTypeID(fileExt);
            fileType = MediaFileUtilities.getFileType(fileExt);
            MediaElement thisMediaFile = new MediaElement();
            thisMediaFile.Source = new Uri(url);
            width = (int)thisMediaFile.Width;
            height = (int)thisMediaFile.Height;
            dateStart = mObject.getStartDate();
            dateEnd = mObject.getEndDate();

            insertQuery = "INSERT INTO Media([url],[filename],[file_extension],[type_id],[width],[height],[date_start],[date_end])"
                    + "VALUES ('" + url + "','" + fileName + "','" + fileExt + "','" + typeID + "','" + width + "','"
                    + height + "','" + dateStart + "','" + dateEnd + "');";
            
            try
            {
                sqlCmd = new SqlCeCommand(insertQuery, sc);
                sqlRdr = sqlCmd.ExecuteReader();
                sqlRdr.Close();
                return true;
            }
            catch (SqlCeException sqlEx)
            {
                MessageBox.Show(sqlEx.Errors.ToString());
                return false;
            }
        }

        //Removes an entry from the media table and returns true if deletion is successful, false otherwise.
        public Boolean removeMedia(MediaObject mObject)
        {
            SqlCeCommand sqlCmd;
            SqlCeDataReader sqlRdr;

            string removeQuery;
            string url, fileName, fileExt;

            url = mObject.getUrl();
            fileName = url.Split('\\').Last();
            fileExt = fileName.Split('.').Last();
            fileName = fileName.Split('.').First();

            removeQuery = "DELETE FROM Media WHERE url = '" + url + "' AND filename = '" + fileName + "' AND file_extension = '" + fileExt + "'";

            try
            {
                sqlCmd = new SqlCeCommand(removeQuery, sc);
                sqlRdr = sqlCmd.ExecuteReader();
                sqlRdr.Close();
                return true;
            }
            catch (SqlCeException sqlEx)
            {
                MessageBox.Show(sqlEx.Errors.ToString());
                return false;
            }
        }

        public string[] grabURLs()
        {
            SqlCeCommand sqlCmd;
            SqlCeDataReader sqlRdr;

            string[] mediaURLs;

            sqlRdr = new SqlCeCommand("SELECT COUNT(*) FROM Media", sc).ExecuteReader();
            sqlRdr.Read();
            int numFiles = (int)sqlRdr[0];
            int count = 0;
            string query = "SELECT url FROM Media;";
            sqlCmd = new SqlCeCommand(query, sc);
            sqlRdr = sqlCmd.ExecuteReader();
            mediaURLs = new string[numFiles];
            while (sqlRdr.Read())
            {
                mediaURLs[count] = sqlRdr["url"].ToString();
                count++;
            }
            return mediaURLs;
        }

        //When user clicks Start Slideshow, the dates selected on the Upload window need to match the dates in the databases
        //Error checking for date ranges was done in UploadMedia.btnStartSlideshow
        internal void updateDateRange(DatePicker dtPickerStart, DatePicker dtPickerEnd)
        {
            SqlCeCommand sqlCmd;
            SqlCeDataReader sqlRdr;

            var updateDates = "UPDATE Media SET date_start = '" + dtPickerStart + "', date_end = '" + dtPickerEnd + "'";
            
            try
            {
                sqlCmd = new SqlCeCommand(updateDates, sc);
                sqlRdr = sqlCmd.ExecuteReader();
                sqlRdr.Close();               
            }
            catch (SqlCeException sqlEx)
            {
                MessageBox.Show(sqlEx.Errors.ToString());
            }
        }
    }
}
