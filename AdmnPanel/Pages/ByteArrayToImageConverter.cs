using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AdmnPanel.Pages
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageData)
            {
                using (var stream = new MemoryStream(imageData))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage bitmapImage)
            {
                using (var memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder;
                    switch (bitmapImage.UriSource?.ToString().ToLower())
                    {
                        case string uri when uri.EndsWith(".png"):
                            encoder = new PngBitmapEncoder();
                            break;
                        case string uri when uri.EndsWith(".jpg") || uri.EndsWith(".jpeg"):
                            encoder = new JpegBitmapEncoder();
                            break;
                        case string uri when uri.EndsWith(".bmp"):
                            encoder = new BmpBitmapEncoder();
                            break;
                        case string uri when uri.EndsWith(".gif"):
                            encoder = new GifBitmapEncoder();
                            break;
                        case string uri when uri.EndsWith(".tiff"):
                            encoder = new TiffBitmapEncoder();
                            break;
                        default:
                            encoder = new PngBitmapEncoder();
                            break;
                    }

                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }
    }
}
