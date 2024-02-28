using QrGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace QrGenerator.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
        public ImageSaver ImageSaver => App.Host.Services.GetRequiredService<ImageSaver>();
    }
}
