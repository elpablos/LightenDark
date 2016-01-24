using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightenDark.Module.Console.Views
{
    /// <summary>
    /// Interaction logic for ConsoleView.xaml
    /// </summary>
    public partial class ConsoleView : UserControl
    {
        public ConsoleView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Automatic scroll down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            ((DataGrid)sender).ScrollIntoView(e.Row.Item);
        }
    }
}
