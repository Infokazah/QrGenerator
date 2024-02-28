using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Drawing;
using QrGenerator.Services.infrastructures;

namespace QrGenerator.Services
{
    public class ImageSaver : IImageSaver
    {
        private BitmapEncoder encoder;

        public ImageSaver()
        {
            encoder = new PngBitmapEncoder();
        }
        public void SaveImageToFile(ImageSource imageSource)
        {
            if (imageSource != null && imageSource is BitmapSource bitmapSource)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                    bitmapSource.PixelWidth, bitmapSource.PixelHeight,
                    bitmapSource.DpiX, bitmapSource.DpiY,
                    PixelFormats.Pbgra32);

                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawImage(bitmapSource, new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
                }
                renderTargetBitmap.Render(drawingVisual);

                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string folderPath = dialog.FileName;
                    try
                    {
                        string filePath = Path.Combine(folderPath, "QrCode.png");
                        encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                        using FileStream fs = new(filePath, FileMode.Create);
                        encoder.Save(fs);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения изображения: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Недопустимый источник изображения");
            }
        }
    }
}
