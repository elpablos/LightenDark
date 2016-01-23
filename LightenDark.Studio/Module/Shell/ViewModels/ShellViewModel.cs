using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;
using LightenDark.Studio.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LightenDark.Studio.Module.Shell.ViewModels
{
    /// <summary>
    /// Overriding of main window
    /// Implementation of closing
    /// </summary>
    [Export(typeof(IShell))]
    public class ShellViewModel : Gemini.Modules.Shell.ViewModels.ShellViewModel
    {
        static ShellViewModel()
        {
            ViewLocator.AddNamespaceMapping(typeof(ShellViewModel).Namespace, typeof(ShellView).Namespace);
        }

        public override void CanClose(Action<bool> callback)
        {
            Coroutine.BeginExecute(CanClose().GetEnumerator(), null, (s, e) => callback(!e.WasCancelled));
        }

        protected override void OnInitialize()
        {
        }

        private IEnumerable<IResult> CanClose()
        {
            yield return new MessageBoxResult();
        }

        private class MessageBoxResult : IResult
        {
            public event EventHandler<ResultCompletionEventArgs> Completed;

            public void Execute(CoroutineExecutionContext context)
            {
                var result = System.Windows.MessageBoxResult.Yes;

                if (Settings.Default.ConfirmExit)
                {
                    result = MessageBox.Show(Resources.CloseDialog_Message, 
                        Resources.CloseDialog_Title, 
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                }

                Completed(this, new ResultCompletionEventArgs { WasCancelled = (result != System.Windows.MessageBoxResult.Yes) });
            }
        }
    }
}
