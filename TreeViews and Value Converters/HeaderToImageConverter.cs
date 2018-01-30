﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeViews_and_Value_Converters
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder, or file
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {

        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //Get the full path
            var path = (string)value;

            //If the path is null, ignore
            if (path == null)
            {
                return null;
            }

            //Get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            //By default, we presume an image
            var image = "Images/file.png";

            //If the name is blank, we presume it is a drive (we cannot have a blank file or folder name)
            if (string.IsNullOrEmpty(name))
            {
                image = "Images/drive.png";
            }
            //Checks if the path is a file directory
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "Images/folder-closed.png";
            }

            //Returning the image
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
