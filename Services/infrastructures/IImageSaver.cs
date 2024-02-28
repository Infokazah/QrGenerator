using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace QrGenerator.Services.infrastructures
{
    interface IImageSaver
    {
        public void SaveImageToFile(ImageSource imageSource);

    }
}
