using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
namespace QrGenerator.Services.infrastructures
{
    internal interface IQrCodeGeneratorService
    {
       public ImageSource GenerateQrCodeUrl(string url, Color back, Color front, ImageSource? source);
    }
}
