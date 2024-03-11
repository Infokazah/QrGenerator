using QRCoder;
using QrGenerator.infrastructure;
using QrGenerator.Services.infrastructures;
using System;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace QrGenerator.Services
{
    internal class QrCodeGeneratorService : IQrCodeGeneratorService
    {   
        private  QRCodeGenerator _qrGenerator;
        public QrCodeGeneratorService() 
        {
            _qrGenerator = new QRCodeGenerator();
        }
        public ImageSource GenerateQrCodeUrl(string url, Color back, Color front, ImageSource? source)
        {
            QRCodeData qrCodeData = _qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap QRCodeBitMap = qrCode.GetGraphic(100, GlobalConverter.ConvertToDrawingColor(front), GlobalConverter.ConvertToDrawingColor(back), true);

            ImageSource QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(QRCodeBitMap.GetHbitmap(),
                                                                             IntPtr.Zero, Int32Rect.Empty,
                                                                             BitmapSizeOptions.FromEmptyOptions());

            if (source != null && source is BitmapSource bitmapSource)
            {
                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext drawingContext = visual.RenderOpen())
                {
                    
                    drawingContext.DrawImage(QRCodeImage, new Rect(0, 0, QRCodeBitMap.Width, QRCodeBitMap.Height));

                    double iconSize = Math.Min(QRCodeBitMap.Width, QRCodeBitMap.Height) / 6; 
                    double iconX = (QRCodeBitMap.Width - iconSize) / 2; 
                    double iconY = (QRCodeBitMap.Height - iconSize) / 2; 

                    drawingContext.DrawImage(bitmapSource, new Rect(iconX, iconY, iconSize, iconSize));
                }

                RenderTargetBitmap targetBitmap = new RenderTargetBitmap(
                    QRCodeBitMap.Width, QRCodeBitMap.Height, 96, 96, PixelFormats.Default);
                targetBitmap.Render(visual);

                QRCodeImage = targetBitmap; 
            }

            return QRCodeImage;
        }


    }
}
