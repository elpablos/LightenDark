using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace LightenDark.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            browser.Address = System.Configuration.ConfigurationManager.AppSettings["DefaultAddress"];

            txtConsole.KeyDown += InputBlock_KeyDown;
            txtConsole.Focus();
        }

        void InputBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var w = this.Parent as MainWindow;
                var vm = w.DataContext as ViewModels.MainViewModel;

                vm.ConsoleMessage = txtConsole.Text;
                vm.JavacriptAction(txtConsole.Text);
                txtConsole.Focus();
            }
        }
    }
}
