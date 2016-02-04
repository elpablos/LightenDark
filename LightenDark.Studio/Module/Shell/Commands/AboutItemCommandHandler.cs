using Gat.Controls;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LightenDark.Studio.Module.Shell.Commands
{

    [CommandHandler]
    public class AboutItemCommandHandler : CommandHandlerBase<AboutItemCommandDefinition>
    {
        public override void Update(Command command)
        {
        }

        public override Task Run(Command command)
        {
            AboutControlView about = new AboutControlView();
            AboutControlViewModel vm = (AboutControlViewModel)about.FindResource("ViewModel");

            // setting several properties here

            System.Windows.Media.Imaging.IconBitmapDecoder ibd =
                new System.Windows.Media.Imaging.IconBitmapDecoder(
                new Uri(@"pack://application:,,/favicon.ico", UriKind.RelativeOrAbsolute),
                System.Windows.Media.Imaging.BitmapCreateOptions.None,
                System.Windows.Media.Imaging.BitmapCacheOption.Default);
            vm.ApplicationLogo = ibd.Frames[0];
            vm.Hyperlink = new Uri(@"http://lightendark.lorenzo.cz/");
            vm.HyperlinkText = "http://lightendark.lorenzo.cz/";
            vm.AdditionalNotes = "Author is not responsible for damages";

            vm.Window.Content = about;
            vm.Window.Show();
            return TaskUtility.Completed;
        }
    }
}
