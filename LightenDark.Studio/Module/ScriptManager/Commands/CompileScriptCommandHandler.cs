using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using Gemini.Modules.CodeCompiler;
using Gemini.Modules.CodeEditor.ViewModels;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Output;
using LightenDark.Api;
using LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager.Commands
{
    [CommandHandler]
    public class CompileScriptCommandHandler : CommandHandlerBase<CompileScriptCommandDefinition>
    {
        [Import]
        public IShell Shell { get; private set; }

        [Import]
        public ICodeCompiler Compiler { get; private set; }

        [Import]
        public IScriptManager ScriptManager { get; private set; }

        [Import]
        public IOutput Output { get; private set; }

        [Import]
        public IErrorList ErrorList { get; private set; }

        public override void Update(Command command)
        {
            if (Shell.ActiveItem != null && Shell.ActiveItem is CodeEditorViewModel)
            {
                var document = Shell.ActiveItem as CodeEditorViewModel;
                command.Enabled = !document.IsDirty && !document.IsNew;
            }
            else
            {
                command.Enabled = false;
            }
        }

        public override Task Run(Command command)
        {
            Shell.ShowTool(Output);
            var document = Shell.ActiveItem as CodeEditorViewModel;
            string script = System.IO.File.ReadAllText(document.FilePath);
            lock (ScriptManager.Items)
            {
                ScriptManager.Items.Clear();

                var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
                var newAssembly = Compiler.Compile(
                     new[] { CSharpSyntaxTree.ParseText(script, CSharpParseOptions.Default, document.FilePath) },
                     new[]
                         {
                            MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")),
                            MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")),
                            MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")),
                            MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Web.Extensions.dll")),
                            MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "Microsoft.CSharp.dll")),
                            MetadataReference.CreateFromFile(typeof(System.Windows.MessageBox).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(IScript).Assembly.Location)
                        },
                    "LightenDarkScript");

                if (newAssembly != null)
                {
                    var items = newAssembly.GetTypes()
                        .Where(x => typeof(IScript).IsAssignableFrom(x) || x.BaseType == typeof(ScriptBase))
                        .Select(x => (IScript)Activator.CreateInstance(x));
                    foreach (var item in items)
                    {
                        ScriptManager.Items.Add(item);
                    }

                    ScriptManager.SelectedItem = ScriptManager.Items.FirstOrDefault();
                }
                else
                {
                    Shell.ShowTool(ErrorList);
                }
            }

            return TaskUtility.Completed;
        }
    }
}
