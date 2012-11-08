using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

/*
 * This Class will act as the interface to our database. 
 * Contains ethods for connecting, disconnecting, inserting, and querying the database.
 */

namespace PilotPlayer
{
    class DataInterface
    {
        SqlConnection sc;

        String appPath;

        public DataInterface()
        {
            appPath = Application.StartupPath;
            sc = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=" + appPath + "\\PPDB.sdf" + ";Integrated Security=True;User Instance=True");

            //Should the Database connection be made here automatically, or explicity by the method below?
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
        public Boolean insertMedia(String absPath, String startDate, String endDate, Int32 type, Int32 width, Int32 Height, float Length)
        {
            //TODO: Break up Absolute Path into FilePath, FileName, and FileExt.
            //      Insert Data into database.
            
            return true;
        }

        //Executes a query passed into the method and returns the result.
        //public SqlDataReader queryDB(/*TODO: Decide on how to pass parameters to this method*/)
        //{
        //    SqlCommand sqlCmd = new SqlCommand();
        //    SqlDataReader results = new SqlDataReader();

        //   // TODO: Execute Query

        //    return results;
        //}

        //Removes an entry from the media table and returns true if deletion is successful, false otherwise.
        public Boolean removeMedia(/*TODO: Decide on how to pass parameters to this method*/)
        {
            
            
            return true;
        }
    }
}
