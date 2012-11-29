using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;

namespace PilotPlayer
{
    /// <summary>
    /// Interaction logic for EditSlideshow.xaml
    /// </summary>
    public partial class EditSlideshow : Window
    {
        DataInterface dbInterface;
        private int startRow = 0;
        Hashtable[] tableData;
        CheckBox[] cbs = new CheckBox[5];
        public EditSlideshow()
        {
            InitializeComponent();
            dbInterface = new DataInterface();
            dbInterface.openConnection();
            tableData = dbInterface.getMediaTable();
            updateEntries();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            dbInterface.openConnection();
            foreach (CheckBox cb in cbs)
            {
                if (tableData.Length > 0)
                {
                    try
                    {
                        if (cb.IsChecked == true)
                        {
                            dbInterface.removeFirstOccurance(cb.Tag as string);
                        }
                    }
                    catch (NullReferenceException nre)
                    {

                    }
                }
            }
            
            updateEntries();
            
        }

        private void addElemToGrid(UIElement element, int row, int col, Grid grid)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, col);
            grid.Children.Add(element);
        }

        private void addRowToGrid(UIElement[] elements, int row, Grid grid)
        {
            int col = 0;
            foreach(UIElement elem in elements)
            {
                addElemToGrid(elem, row, col, grid);
                col++;
            }
        }

        private void updateEntries()
        {
            tableData = dbInterface.getMediaTable();
            if (tableData.Length == 0)
            {
                grid.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                grid.Visibility = Visibility.Visible;
            }
            int row = 0;
            grid.RowDefinitions.Clear();
            for (int i = startRow; (i < startRow + 5) && (i < tableData.Length); i++)
            {
                cbs[row] = new CheckBox();
                cbs[row].Tag = tableData[i]["url"].ToString();
                cbs[row].Checked += new RoutedEventHandler(cb_checked);
                grid.RowDefinitions.Add(new RowDefinition());
                TextBox filename = new TextBox();
                filename.IsReadOnly = true;
                filename.Text = tableData[i]["filename"].ToString();
                TextBox filetype = new TextBox();
                filetype.IsReadOnly = true;
                filetype.Text = MediaFileUtilities.getFileType(tableData[i]["file_extension"].ToString());
                DatePicker dateStart = new DatePicker();
                DatePicker dateEnd = new DatePicker();
                object[] tags = new object[2];
                dateStart.SelectedDate = DateTime.Parse(tableData[i]["date_start"].ToString());
                dateEnd.SelectedDate = DateTime.Parse(tableData[i]["date_end"].ToString());
                tags[0] = tableData[i]["url"].ToString();
                tags[1] = dateEnd;
                dateStart.Tag = tags;
                tags[0] = tableData[i]["url"].ToString();
                tags[1] = dateEnd;
                dateEnd.Tag = tags;
                dateStart.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(dateStart_SourceUpdated);
                dateEnd.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(dateEnd_SourceUpdated);
                dateEnd.DisplayDateStart = dateStart.SelectedDate;
                UIElement[] rowItems = { cbs[row], filename, filetype, dateStart, dateEnd };

                addRowToGrid(rowItems, row, grid);
                row++;
            }

            if (startRow == 0)
            {
                pageBack.IsEnabled = false;
                pageBack.Opacity = 0.5;
            }
            else
            {
                pageBack.IsEnabled = true;
                pageBack.Opacity = 1;
            }

            if (startRow >= tableData.Length - 5)
            {
                Console.WriteLine("Boom");
                pageForward.IsEnabled = false;
                pageForward.Opacity = 0.5;
            }
            else
            {
                pageForward.IsEnabled = true;
                pageForward.Opacity = 1;
            }
        }

        private void pageForward_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startRow += 5;
            updateEntries();

        }

        private void pageBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (startRow > 0)
            {
                startRow -= 5;
                updateEntries();
            }
        }

        private void cb_checked(object sender, EventArgs e)
        {
            
        }

        private void dateStart_SourceUpdated(object sender, EventArgs e)
        {
            DatePicker dp = sender as DatePicker;
            object[] tags = dp.Tag as object[];
            string url = tags[0] as string;
            DatePicker endDatePicker = tags[1] as DatePicker;

            dbInterface.update("date_start", dp.SelectedDate.ToString(), " url = '" + url + "'");
            endDatePicker.DisplayDateStart = dp.SelectedDate;
            if (dp.SelectedDate > endDatePicker.SelectedDate)
            {
                endDatePicker.SelectedDate = dp.SelectedDate;
            }
            Console.WriteLine("Update " + dp.Tag.ToString() + ", set date_start=" + dp.SelectedDate.ToString());
        }

        private void dateEnd_SourceUpdated(object sender, EventArgs e)
        {
            DatePicker dp = sender as DatePicker;
            object[] tags = dp.Tag as object[];
            string url = tags[0] as string;
            DatePicker startDatePicker = tags[1] as DatePicker;

            dbInterface.update("date_end", dp.SelectedDate.ToString(), " url = '" + url + "'");
            Console.WriteLine("Update " + dp.Tag.ToString() + ", set date_end=" + dp.SelectedDate.ToString());
        }
    }
}
