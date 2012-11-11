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
        private System.Uri mediaURI;
        private int currentSelection;

        public MainWindow(DatePicker dtPickerStart, DatePicker dtPickerEnd)
        {
            this.dtPickerStart = dtPickerStart;
            this.dtPickerEnd = dtPickerEnd;

            InitializeComponent();
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            Topmost = true;

            WindowState = WindowState.Maximized;
            mElement.LoadedBehavior = MediaState.Manual;
            mElement.Clock = null;
            currentSelection = 0;
        }

        public void StartSlideshow()
        {
            DataInterface dbInterface = new DataInterface();
            dbInterface.openConnection();
            dbInterface.updateDateRange(dtPickerStart, dtPickerEnd);

            mediaList = dbInterface.grabURLs();

            mElement.MediaEnded += new RoutedEventHandler(mElement_MediaEnded);
            mElement.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(mElement_MediaFailed);
            mElement.MediaOpened += new RoutedEventHandler(mElement_MediaOpened);

            loadMedia();
        }

        private void loadMedia()
        {
            mediaURI = new System.Uri(mediaList[currentSelection]);
            mElement.Source = mediaURI;
            mElement.Width = this.Width;
            mElement.Height = this.Height;
            mObject = new MediaObject(mElement);

            mObject.playVideoMedia();
        }

        private void mElement_MediaOpened(object sender, RoutedEventArgs e)
        {
        }

        private void mElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //Do nothing. If error we'll try to press on and play/show
        }

        private void mElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (currentSelection + 1 < mediaList.Length)
            {
                currentSelection += 1;
            }
            else
            {
                currentSelection = 0;
            }
            loadMedia();
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
