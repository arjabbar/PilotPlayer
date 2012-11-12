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
        int maxRow = 0, startRow = 0;
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
            ArrayList idsToDelete = new ArrayList();
            dbInterface.openConnection();
            for (int i = 0; (i < startRow + 5) && (i < tableData.Length); i++)
            {
                if (cbs[i].IsChecked == true)
                {
                    idsToDelete.Add(tableData[i + startRow]["media_id"]);
                }
            }
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
            
            int row = 0;
            grid.RowDefinitions.Clear();
            for (int i = startRow; (i < startRow + 5) && (i < tableData.Length); i++)
            {
                cbs[i] = new CheckBox();
                cbs[i].Checked += new RoutedEventHandler(cb_checked);
                grid.RowDefinitions.Add(new RowDefinition());
                TextBox filename = new TextBox();
                filename.Text = tableData[i]["filename"].ToString();
                TextBox filetype = new TextBox();
                filetype.Text = MediaFileUtilities.getFileType(tableData[i]["file_extension"].ToString());
                DatePicker dateStart = new DatePicker();
                dateStart.SelectedDate = DateTime.Parse(tableData[i]["date_start"].ToString());
                DatePicker dateEnd = new DatePicker();
                dateEnd.SelectedDate = DateTime.Parse(tableData[i]["date_end"].ToString());
                UIElement[] rowItems = { cbs[i], filename, filetype, dateStart, dateEnd };

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
    }
}
