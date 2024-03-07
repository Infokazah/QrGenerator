using QRCoder;
using QrGenerator.infrastructure;
using QrGenerator.Services.infrastructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ImageSource GenerateQrCodeImage(string QrImage, Color back, Color front, string? source)
        {
                // Чтение изображения в байтовый массив
                byte[] imageBytes = System.IO.File.ReadAllBytes(QrImage);

                // Преобразование байтов в строку Base64
                string base64String = Convert.ToBase64String(imageBytes);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(base64String, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Генерация изображения QR-кода
                Bitmap QRCodeBitMap = qrCode.GetGraphic(100, System.Drawing.Color.FromArgb(back.A, back.R, back.G, back.B),
                                                         System.Drawing.Color.FromArgb(front.A, front.R, front.G, front.B), true);

                // Добавление логотипа, если он указан
                if (source != null)
                {
                    using (Graphics graphics = Graphics.FromImage(QRCodeBitMap))
                    {
                        Bitmap logo = new Bitmap(source);
                        int logoSizeX = QRCodeBitMap.Width / 6;
                        int logoSizeY = QRCodeBitMap.Height / 6;
                        int logoPositionX = (QRCodeBitMap.Width - logoSizeX) / 2;
                        int logoPositionY = (QRCodeBitMap.Height - logoSizeY) / 2;

                        graphics.DrawImage(logo, new Rectangle(logoPositionX, logoPositionY, logoSizeX, logoSizeY));
                    }
                }

                // Преобразование Bitmap в ImageSource
                ImageSource QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(QRCodeBitMap.GetHbitmap(),
                                                                                 IntPtr.Zero, Int32Rect.Empty,
                                                                                 BitmapSizeOptions.FromEmptyOptions());
            return QRCodeImage;
        }

        public ImageSource GenerateQrCodeUrl(string url, Color back, Color front, string? source)
        {
                QRCodeData qrCodeData = _qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap QRCodeBitMap = qrCode.GetGraphic(100, GlobalConverter.ConvertToDrawingColor(back), GlobalConverter.ConvertToDrawingColor(front), true);

                if (source != null)
                {
                    using (Graphics graphics = Graphics.FromImage(QRCodeBitMap))
                    {
                        Bitmap logo = new Bitmap(source);
                        int logoSizeX = QRCodeBitMap.Width / 6;
                        int logoSizeY = QRCodeBitMap.Height / 6;
                        int logoPositionX = (QRCodeBitMap.Width - logoSizeX) / 2;
                        int logoPositionY = (QRCodeBitMap.Height - logoSizeY) / 2;

                        graphics.DrawImage(logo, new Rectangle(logoPositionX, logoPositionY, logoSizeX, logoSizeY));
                    }
                }

                ImageSource QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(QRCodeBitMap.GetHbitmap(),
                        IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return QRCodeImage;
            
        }
    }
}
