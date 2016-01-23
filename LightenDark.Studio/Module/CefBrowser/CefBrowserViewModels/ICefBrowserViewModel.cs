using Gemini.Framework;

namespace LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels
{
    /// <summary>
    /// Inteface for communicating with CEF browser
    /// for example, sending JS into website
    /// </summary>
    public interface ICefBrowserViewModel: IDocument
    {
        void ExecuteJavaScriptAsync(string message);
    }
}