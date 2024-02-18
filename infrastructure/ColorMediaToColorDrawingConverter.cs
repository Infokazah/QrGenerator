using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrGenerator.infrastructure
{
    internal static class ColorMediaToColorDrawingConverter
    {
        public static Color ConvertToDrawingColor(this System.Windows.Media.Color wpfColor)
        {
            return Color.FromArgb(wpfColor.A, wpfColor.R, wpfColor.G, wpfColor.B);
        }
    }
}
