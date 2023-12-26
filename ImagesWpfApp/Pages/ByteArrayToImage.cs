using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ImagesWpfApp.Pages
{
    public class ByteArrayToImage:IValueConverter
    {
        public object Convert(object value, Type targetType, object type, CultureInfo culture)
        {
            byte[] byteArray = value as byte[];
            if (byteArray != null && byteArray.Length > 0)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(byteArray);
                bitmapImage.EndInit();

                return bitmapImage;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
