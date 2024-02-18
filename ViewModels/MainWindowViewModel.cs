using BaseClassesLyb;
using Microsoft.Win32;
using QRCoder;
using QrGenerator.infrastructure;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;


namespace QrGenerator.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ImageSource _qrCodeImage;

        private int _qrSize;

        public int QrSize
        {
            get => _qrSize;

            set
            {
                _qrSize = value;
                OnPropertyChanged(nameof(QrSize));
            }

        }

        private Color _backColor;

        public Color BackColor
        {
            get => _backColor;

            set
            {
                _backColor = value;
                OnPropertyChanged(nameof(BackColor));
            }

        }

        private Color _frontColor;

        public Color FrontColor
        {
            get => _frontColor;

            set
            {
                _frontColor = value;
                OnPropertyChanged(nameof(FrontColor));
            }
        }
        public ImageSource QRCodeImage
        {
            get => _qrCodeImage;

            set 
            {
                _qrCodeImage = value;
                OnPropertyChanged(nameof(QRCodeImage));
            }
        }
        private QRCodeGenerator _qrGenerator;
        public RegularCommand GenerateQrCode { get; }
        private bool CanGenerateQrCodeExecute(object p) => true;

        private void GenerateQrCodeExecute(object str)
        {
            if (str is string stroke)
            {

                QRCodeData qrCodeData = _qrGenerator.CreateQrCode(stroke, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap QRCodeBitMap = qrCode.GetGraphic(QrSize, ColorMediaToColorDrawingConverter.ConvertToDrawingColor(BackColor), ColorMediaToColorDrawingConverter.ConvertToDrawingColor(FrontColor), true);
                QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(QRCodeBitMap.GetHbitmap(), 
                    IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public RegularCommand DownLoadIconCode { get; }
        private bool CanDownLoadIconExecute(object p) => true;

        private void DownLoadIconExecute()
        {
           
        }
        public MainWindowViewModel() 
        {
            _qrGenerator = new QRCodeGenerator();
            GenerateQrCode = new RegularCommand(GenerateQrCodeExecute, CanGenerateQrCodeExecute);
        }
    }
}
