using BaseClassesLyb;
using Microsoft.Win32;
using QRCoder;
using QrGenerator.infrastructure;
using QrGenerator.Services;
using QrGenerator.Services.infrastructures;
using System;
using System.Windows;
using System.Windows.Media;
using WpfBaseLyb;
using Color = System.Windows.Media.Color;


namespace QrGenerator.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        //сервисы
        private ImageSaver _imageSaver;
        private QrCodeGeneratorService _qrCodeGeneratorService;

        #region атрибуты        
        //изображение QrCode
        private ImageSource _qrCodeImage;
        public ImageSource QRCodeImage
        {
            get => _qrCodeImage;

            set
            {
                _qrCodeImage = value;
                OnPropertyChanged(nameof(QRCodeImage));
            }
        }
        //Изображение иконки для QrCode
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

        //задний фон
        private Color _backColor = Color.FromRgb(0,0,0);

        public Color BackColor
        {
            get => _backColor;

            set
            {
                _backColor = value;
                OnPropertyChanged(nameof(BackColor));
            }

        }
        //передний фон
        private Color _frontColor = Color.FromRgb(255, 255, 255);

        public Color FrontColor
        {
            get => _frontColor;

            set
            {
                _frontColor = value;
                OnPropertyChanged(nameof(FrontColor));
            }
        }
        #endregion
        #region Команды
        public RegularCommand GenerateQrCode { get; }
        private bool CanGenerateQrCodeExecute(object p) => true;

        private void GenerateQrCodeExecute(object str)
        {
            QRCodeImage = _qrCodeGeneratorService.GenerateQrCodeUrl(str.ToString(), BackColor, FrontColor, QrCodeIcon);
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

        public SimpleCommand RemoveIcon { get; }
        private bool CanRemoveIconExecute() => true;

        private void RemoveIconExecute()
        {
            QrCodeIcon = null;
        }
        #endregion 
        public MainWindowViewModel(IImageSaver imageSaver, IQrCodeGeneratorService qrCodeGeneratorService) 
        {
            _imageSaver = (ImageSaver?)imageSaver;
            _qrCodeGeneratorService = (QrCodeGeneratorService?)qrCodeGeneratorService;

            GenerateQrCode = new RegularCommand(GenerateQrCodeExecute, CanGenerateQrCodeExecute);
            DownLoadIconCode = new SimpleCommand(DownLoadIconExecute, CanDownLoadIconExecute);
            DownLoadQrCode = new SimpleCommand(DownLoadQrCodeExecute, CanDownLoadQrCodeExecute);
            RemoveIcon = new SimpleCommand(RemoveIconExecute, CanRemoveIconExecute);
        }
    }
}
