using BaseClassesLyb;
using Microsoft.Win32;
using QRCoder;
using QrGenerator.infrastructure;
using QrGenerator.Services;
using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using WpfBaseLyb;
using Brush = System.Drawing.Brush;
using Color = System.Windows.Media.Color;


namespace QrGenerator.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ImageSaver _imageSaver;
        private ImageSource _qrCodeImage;

        private ImageSource _qrCodeIcon;

        public ImageSource QrCodeIcon
        {
            get => _qrCodeIcon;

            set
            {
                _qrCodeIcon = value;
                OnPropertyChanged(nameof(QrCodeIcon));
            }
        }

        private string QrIconPath;

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
                Bitmap QRCodeBitMap = qrCode.GetGraphic(100, GlobalConverter.ConvertToDrawingColor(BackColor), GlobalConverter.ConvertToDrawingColor(FrontColor), true);


                using (Graphics graphics = Graphics.FromImage(QRCodeBitMap))
                {
                    Bitmap logo = new Bitmap(QrIconPath);
                    int logoSizeX = QRCodeBitMap.Width / 6;
                    int logoSizeY = QRCodeBitMap.Height / 6;
                    int logoPositionX = (QRCodeBitMap.Width - logoSizeX) / 2;
                    int logoPositionY = (QRCodeBitMap.Height - logoSizeY) / 2;

                    graphics.DrawImage(logo, new Rectangle(logoPositionX, logoPositionY, logoSizeX, logoSizeY));
                }

                QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(QRCodeBitMap.GetHbitmap(),
                        IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public SimpleCommand DownLoadIconCode { get; }
        private bool CanDownLoadIconExecute() => true;

        private void DownLoadIconExecute()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFile = openFileDialog.FileName;
                    QrCodeIcon = GlobalConverter.ConvertStringToImageSource(selectedFile);
                    QrIconPath = openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("Файлы не выбраны.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }

        }

        public SimpleCommand DownLoadQrCode { get; }
        private bool CanDownLoadQrCodeExecute() => true;

        private void DownLoadQrCodeExecute()
        {
            _imageSaver.SaveImageToFile(QRCodeImage);
        }
        public MainWindowViewModel() 
        {
            _qrGenerator = new QRCodeGenerator();
            _imageSaver = new ImageSaver();
            GenerateQrCode = new RegularCommand(GenerateQrCodeExecute, CanGenerateQrCodeExecute);
            DownLoadIconCode = new SimpleCommand(DownLoadIconExecute, CanDownLoadIconExecute);
            DownLoadQrCode = new SimpleCommand(DownLoadQrCodeExecute, CanDownLoadQrCodeExecute);
        }
    }
}
