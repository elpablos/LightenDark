using CefSharp;
using LightenDark.Api.Enums;
using LightenDark.Interfaces;

namespace LightenDark.Interop
{
    /// <summary>
    /// Třída propojující JS s C#
    /// </summary>
    public class BoundClass
    {
        [JavascriptIgnore]
        public IMainViewModel Vm { get; set; }

        public BoundClass(IMainViewModel vm)
        {
            Vm = vm;
        }

        /// <summary>
        /// Logování webSocketu
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketData(string data)
        {
            Vm.LogNewAction(ApplicationMessageType.In, data);
        }

        /// <summary>
        /// Logování odesílání
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketSend(string data)
        {
            Vm.LogNewAction(ApplicationMessageType.Out, data);
        }

        public void OnSelected(string selected)
        {
            // MessageBox.Show(selected);
        }

        public void OnDocumentReady()
        {
            Vm.LogNewAction(ApplicationMessageType.Console, "DocumentReady");
        }
    }
}
