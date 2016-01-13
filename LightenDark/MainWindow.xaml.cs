using LightenDark.Interfaces;
using LightenDark.ViewModels;
using System.Windows;
using System.ComponentModel;
using System;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using System.IO;

namespace LightenDark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SaveDockFile = "dock.layout";

        public IMainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            Title = "LightenDark v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (File.Exists(SaveDockFile))
            {
                XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MainViewControl.DockManager);
                using (var reader = new StreamReader(SaveDockFile))
                {
                    layoutSerializer.Deserialize(reader);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MainViewControl.DockManager);
            using (var writer = new StreamWriter(SaveDockFile))
            {
                layoutSerializer.Serialize(writer);
            }

            base.OnClosing(e);
        }
    }
}
