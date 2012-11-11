using System;
using System.Collections.Generic;
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
        private String[] mediaList;
        private DatePicker dtPickerStart;
        private DatePicker dtPickerEnd;

        public MainWindow(DatePicker dtPickerStart, DatePicker dtPickerEnd)
        {
            this.dtPickerStart = dtPickerStart;
            this.dtPickerEnd = dtPickerEnd;

            InitializeComponent();
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            Topmost = true;

            //WindowState = WindowState.Maximized;
            mElement.LoadedBehavior = MediaState.Manual;
            mElement.Clock = null;
        }

        public void StartSlideshow()
        {
            DataInterface dbInterface = new DataInterface();
            dbInterface.openConnection();
            dbInterface.updateDateRange(dtPickerStart, dtPickerEnd);

            mediaList = dbInterface.grabURLs();

            for (int i = 0; i < mediaList.Length; i++)
            {
                System.Uri mediaURI = new System.Uri(mediaList[i]);

                mElement.Source = mediaURI;
                mObject = new MediaObject(mElement);
                mObject.playVideoMedia();

            }        
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
