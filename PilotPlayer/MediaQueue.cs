using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * This class is a specialized Queue ADT for our application.
 * It represents a Queue of Media Objects.
 */
namespace PilotPlayer
{
    class MediaQueue
    {
        enum SortType {Asc, Desc, Rand};

        ArrayList mediaQueue;
        SortType sortMode;

        public MediaQueue()
        {
            mediaQueue = new ArrayList();
            
            sortMode = SortType.Asc;
        }

        public Int32 getSize()
        {
            return mediaQueue.Count;
        }

        public Boolean addMedia(MediaObject mObject)
        {
            try
            {
                mediaQueue.Add(mObject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Needs work. User probably won't pass an index to the queue.
        public Boolean removeMedia(Int32 index)
        {
            try
            {
                mediaQueue.RemoveAt(index);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void setStartDate()
        {
        }

        public void setEndDate()
        {
        }

        public void setSortType()
        {
        }

        private void sortQueue()
        {
        }

        private void sortByDateAsc()
        {
        }

        private void sortByDateDesc()
        {
        }

        private void sortByDateRandom()
        {
        }
    }
}
