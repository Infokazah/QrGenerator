using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QrGenerator.infrastructure
{
    internal static class GlobalConverter
    {
        public static System.Drawing.Color ConvertToDrawingColor(this System.Windows.Media.Color wpfColor)
        {
            return System.Drawing.Color.FromArgb(wpfColor.A, wpfColor.R, wpfColor.G, wpfColor.B);
        }

        public static ImageSource ConvertStringToImageSource(string imagePath)
        {
            try
            {
                BitmapImage imageSource = new BitmapImage();

                imageSource.BeginInit();
                imageSource.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                imageSource.EndInit();

                return imageSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при загрузке изображения: " + ex.Message);
                return null;
            }
        }
    }
}
