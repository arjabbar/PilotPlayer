using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilotPlayer
{
    class MediaFileUtilities
    {
        private static string[] imageTypes = { "jpg", "jpeg", "png", "bmp", "gif" };
        private static string[] videoTypes = { "wmv", "avi", "mp4", "flv", "3gp", "swf" };
        private static string[] audioTypes = { "mp3", "ogg", "wav", "m4a", "mid", "wma" };
        public static char[] sepEsc = { '\\' };
        public static char[] sepDot = { '.' };

        public static string getFileType(string extension)
        {
            if (isInArray(extension, imageTypes))
            {
                return "Image";
            }
            else if (isInArray(extension, videoTypes))
            {
                return "Video";
            }
            else if (isInArray(extension, audioTypes))
            {
                return "Audio";
            }
            else
            {
                return "Other";
            }
        }

        public static int getFileTypeID(string extension)
        {
            if (isInArray(extension, imageTypes))
            {
                return 2;
            }
            else if (isInArray(extension, videoTypes))
            {
                return 1;
            }
            else if (isInArray(extension, audioTypes))
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public static Boolean isInArray<T>(T element, T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (element.ToString() == array[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }
}