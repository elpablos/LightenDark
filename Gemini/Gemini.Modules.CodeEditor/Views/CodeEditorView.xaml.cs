using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;

namespace Gemini.Modules.CodeEditor.Views
{
    public partial class CodeEditorView : UserControl, ICodeEditorView
    {
        public TextEditor TextEditor
        {
            get { return CodeEditor; }
        }

        public CodeEditorView()
        {
            InitializeComponent();
            // commented because it cause crash when is dock dropped in different place!
            // Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
