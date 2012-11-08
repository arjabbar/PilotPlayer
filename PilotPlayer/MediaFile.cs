using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace PilotPlayer
{
    class MediaFile
    {
        private string insertQuery;
        private string url, fileName, fileExt, fileType;
        private int typeID, width, height, length;
        private DateTime dateStart, dateEnd;

        public MediaFile(string path, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine(path + startDate.ToString());
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

        public bool insertMediaFile(SqlCeConnection sc)
        {
            Console.WriteLine(sc.ConnectionString);
            sc.Open();
            SqlCeDataReader sqlRdr;
            try
            {
                SqlCeCommand sqlCmd = new SqlCeCommand(insertQuery, sc);
                sqlRdr = sqlCmd.ExecuteReader();
                sqlRdr.Close();
                return true;
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
        }

    }
}