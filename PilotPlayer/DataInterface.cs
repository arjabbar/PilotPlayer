using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Windows.Controls;
using System.Collections;
using System.Speech.Synthesis;

/*
 * This Class will act as the interface to our database. 
 * Contains ethods for connecting, disconnecting, inserting, and querying the database.
 */

namespace PilotPlayer
{
    class DataInterface
    {
        SqlCeConnection sc;
        SqlCeCommand sqlCmd;
        SqlCeDataReader sqlRdr;
        String appPath;
        public enum Order { ASC, DESC};
        SpeechSynthesizer speech;
        public bool speechOn = false;

        public DataInterface()
        {
            appPath = Application.StartupPath;
            sc = new SqlCeConnection("Data Source=" + appPath + "\\..\\..\\PilotPlayerDB.sdf" + ";Persist Security Info=False;");
            openConnection();
            removeDups();
            speech = new SpeechSynthesizer();
            foreach (string record in grabActiveURLs())
            {
                Console.WriteLine(record);
            }
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

        public Boolean removeFirstOccurance(string url)
        {

            string removeQuery;

            removeQuery = "DELETE FROM Media WHERE media_id IN (SELECT TOP(1) media_id FROM Media WHERE url = '" + url + "' ORDER BY date_end ASC)";

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

        public string[] grabActiveURLs()
        {

            ArrayList mediaURLs = new ArrayList();
            string query = "SELECT * FROM Media;";
            sqlCmd = new SqlCeCommand(query, sc);
            sqlRdr = sqlCmd.ExecuteReader();
            while (sqlRdr.Read())
            {
                if ((DateTime.Parse(sqlRdr["date_start"].ToString()) <= DateTime.Now) && 
                    (DateTime.Parse(sqlRdr["date_end"].ToString()) >= DateTime.Now))
                {
                    Console.WriteLine("Okay");
                    mediaURLs.Add(sqlRdr["url"].ToString());
                }
            }
            return mediaURLs.ToArray(typeof(string)) as string[];
        }

        public Hashtable[] getMediaTable()
        {
            
            Hashtable[] table;

            sqlRdr = new SqlCeCommand("SELECT COUNT(*) FROM Media", sc).ExecuteReader();
            sqlRdr.Read();
            int numFiles = (int)sqlRdr[0];
            table = new Hashtable[numFiles];
            int rowCount = 0;
            string query = "SELECT * FROM Media;";
            sqlCmd = new SqlCeCommand(query, sc);
            sqlRdr = sqlCmd.ExecuteReader();
            while (sqlRdr.Read())
            {
                table[rowCount] = new Hashtable();
                table[rowCount]["media_id"] = (int)sqlRdr["media_id"];
                table[rowCount]["url"] = (string)sqlRdr["url"];
                table[rowCount]["filename"] = (string)sqlRdr["filename"];
                table[rowCount]["file_extension"] = (string)sqlRdr["file_extension"];
                table[rowCount]["type_id"] = (int)sqlRdr["type_id"];
                table[rowCount]["width"] = (int)sqlRdr["width"];
                table[rowCount]["height"] = (int)sqlRdr["height"];
                table[rowCount]["date_start"] = DateTime.Parse(sqlRdr["date_start"].ToString());
                table[rowCount]["date_end"] = DateTime.Parse(sqlRdr["date_end"].ToString());
                rowCount++;
            }
            Console.WriteLine(table.Length);
            return table;
        }

        public Hashtable[] getMediaTable(string orderBy, Order order)
        {

            Hashtable[] table;

            sqlRdr = new SqlCeCommand("SELECT COUNT(*) FROM Media", sc).ExecuteReader();
            sqlRdr.Read();
            int numFiles = (int)sqlRdr[0];
            table = new Hashtable[numFiles];
            int rowCount = 0;
            string query = "SELECT * FROM Media ORDER BY " + orderBy + " " + Enum.GetName(typeof(Order), order);
            sqlCmd = new SqlCeCommand(query, sc);
            sqlRdr = sqlCmd.ExecuteReader();
            while (sqlRdr.Read())
            {
                table[rowCount] = new Hashtable();
                table[rowCount]["media_id"] = (int)sqlRdr["media_id"];
                table[rowCount]["url"] = (string)sqlRdr["url"];
                table[rowCount]["filename"] = (string)sqlRdr["filename"];
                table[rowCount]["file_extension"] = (string)sqlRdr["file_extension"];
                table[rowCount]["type_id"] = (int)sqlRdr["type_id"];
                table[rowCount]["width"] = (int)sqlRdr["width"];
                table[rowCount]["height"] = (int)sqlRdr["height"];
                table[rowCount]["date_start"] = DateTime.Parse(sqlRdr["date_start"].ToString());
                table[rowCount]["date_end"] = DateTime.Parse(sqlRdr["date_end"].ToString());
                rowCount++;
            }
            Console.WriteLine(table.Length);
            return table;
        }

        //Method that speeks the file name of the media about to be played
        public void sayFileName(String fileName)
        {
            if (speechOn)
                speech.Speak(fileName);
        }

        public int update(string column, string newValue, params string[] conditions)
        {

            string query = "UPDATE Media SET " + column + " = '" + newValue + "' WHERE (";
            foreach (string cond in conditions)
            {
                query += (cond.Equals(conditions.Last<string>())) ? cond + ") " : cond + ") AND (";
            }

            sqlCmd = new SqlCeCommand(query, sc);
            return sqlCmd.ExecuteNonQuery();
        }

        public void removeDups()
        {
            Hashtable[] table = getMediaTable("date_end", Order.DESC);
            for (int rowA = 0; rowA < table.Length; rowA++)
            {
                for (int rowB = 0; rowB < table.Length; rowB++)
                {
                    if (rowA == rowB)
                    {
                        break;
                    }
                    else
                    {
                        if (table[rowA]["url"].ToString()==table[rowB]["url"].ToString())
                        {
                            sqlRdr = executeQuery("SELECT COUNT(*) FROM Media WHERE url = '" + table[rowA]["url"].ToString() + "'");
                            sqlRdr.Read();
                            int numDups = (int)sqlRdr[0];
                            while (numDups > 1)
                            {
                                Console.WriteLine("Deleting dups.");
                                removeFirstOccurance(table[rowA]["url"].ToString());
                                numDups--;
                            }
                        }
                    }
                }
            }
        }

        public SqlCeDataReader executeQuery(string query)
        {
            sqlCmd = new SqlCeCommand(query, sc);
            return sqlCmd.ExecuteReader();
        }

        public int executeNonQuery(string query)
        {
            sqlCmd = new SqlCeCommand(query, sc);
            return sqlCmd.ExecuteNonQuery();
        }
    }
}
