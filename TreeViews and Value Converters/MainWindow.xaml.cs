using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace TreeViews_and_Value_Converters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //When the window is opened
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //For each logical drive that is available
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a TreeViewItem called item
                var item = new TreeViewItem()
                {
                    //Setting the item Header to the drive letter
                    Header = drive,
                    //Setting the item Tag to the full path
                    Tag = drive
                    
                };

                //Adding a dummy item so we can expand the drive
                item.Items.Add(null);

                //Listen out for an item being expanded

                item.Expanded += Folder_Expanded;

                //Add the item to the TreeView called FolderView
                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {

            var item = (TreeViewItem)sender;

            //If the item only contains the dummy data, 
            if (item.Items.Count != 1 && item.Items[0] == null)
            {
                return;
            }

            //Clear dummy data
            item.Items.Clear();

            //Get full path
            var fullPath = (string)item.Tag;

            //Create a blank list for directories
            var directories = new List<string>();

            //Try and get directories from the folder
            //Ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }

            directories.ForEach(directoryPath =>
            {

                //Create directory item
                var subItem = new TreeViewItem()
                {
                    //Set Header as folder name
                    Header = Path.GetDirectoryName(directoryPath),
                    //Set Tag as full path
                    Tag = directoryPath

                };

                //Add dummy item so we can expand the folder
                subItem.Items.Add(null);

                //Handle expanding
                subItem.Expanded += Folder_Expanded;

                //Add this item to the parent
                item.Items.Add(subItem);



            });
            

        }
    }
}
