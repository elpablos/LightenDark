using LightenDark.Api.Interfaces;
using Microsoft.CSharp;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LightenDark.Interop
{
    public class RuntimeCompiler
    {
        public static Version DotNetVersion
        {
            get
            {
                List<Version> versions = new List<Version>();

                string fwSetup = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\";
                string HKLM = Registry.LocalMachine.ToString() + "\\";
                using (RegistryKey fwInstallKey = Registry.LocalMachine.OpenSubKey(fwSetup))
                {
                    foreach (string vers in fwInstallKey.GetSubKeyNames())
                    {
                        string fwVersion = (string)Registry.GetValue(HKLM + fwSetup + vers, "Version", null);
                        if (fwVersion != null)
                        {
                            try
                            {
                                // Trace.WriteLine(fwVersion, "Runtime");
                                versions.Add(new Version(fwVersion));
                            }
                            catch { }
                        }
                    }
                }

                if (versions.Count > 0)
                {
                    versions.Sort();
                    return versions[versions.Count - 1];
                }
                else {
                    return new Version();
                }
            }
        }

        public static bool Net35
        {
            get
            {
                return DotNetVersion >= new Version(3, 5);
            }
        }

        public static FileInfo GetAssemblyFileInfo(System.Reflection.Assembly assembly)
        {
            return new FileInfo(new Uri(assembly.CodeBase).LocalPath);
        }

        public List<CompilerError> Errors { get; set; }

        public IScript Compile(string script, ICore core)
        {
            MethodInfo ExecutableMethod = null;
            Type ExecutableClass = null;
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source.cs");
            string refAssemblyName = GetAssemblyFileInfo(typeof(ICore).Assembly).FullName;

            System.IO.File.WriteAllText(path, script);
            var result = Compile(new string[] { path }, new string[] { refAssemblyName });

            if (!result.Errors.HasErrors)
            {
                var inMemoryAssembly = result.CompiledAssembly;
                foreach (Type t in inMemoryAssembly.GetTypes())
                {
                    if (t.IsAssignableFrom(typeof(IScript)) || t.BaseType == typeof(Api.Scripts.AbstractScript))
                    {
                        ExecutableClass = t;
                        break;
                    }

                }

                if (ExecutableClass != null)
                {
                    var instance = (IScript)inMemoryAssembly.CreateInstance(ExecutableClass.FullName);
                    instance.Core = core;
                    return instance;
                }
            }
            else
            {

                Errors = new List<CompilerError>();
                string errz = string.Empty;
                foreach (System.CodeDom.Compiler.CompilerError err in result.Errors)
                {
                    Errors.Add(err);
                }
            }

            return null;
        }

        /// <summary>
        /// Kompilace v paměti 
        /// reálně se ale vše kompiluje do tempu
        /// TODO problém s uvolnovanim !
        /// sourceFiles = zdroják
        /// referencedAssemblies = assembly.FullName
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="sourceFiles"></param>
        /// <param name="referencedAssemblies"></param>
        /// <returns></returns>
        public static CompilerResults Compile(string[] sourceFiles, string[] referencedAssemblies)
        {
            CodeDomProvider provider;

            Dictionary<string, string> args = new Dictionary<string, string>();

            Trace.WriteLine(".NET runtime version: " + Environment.Version.ToString(), "Runtime");
            Trace.WriteLine(".NET installed version: " + DotNetVersion.ToString(), "Runtime");
            if (Net35)
            {
                args.Add("CompilerVersion", "v3.5");
                Trace.WriteLine("Using v3.5 compiler", "Runtime");
            }


            provider = new CSharpCodeProvider(args);
            Debug.Assert(provider.FileExtension.ToLowerInvariant() == "cs");

            CompilerParameters options = new CompilerParameters();
            options.GenerateExecutable = false;
            options.GenerateInMemory = true;
            options.IncludeDebugInformation = true;
            options.WarningLevel = 3;

            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            options.ReferencedAssemblies.Add("System.Drawing.dll");
            options.ReferencedAssemblies.Add("System.Xml.dll");
            options.ReferencedAssemblies.Add("System.Data.dll");

            if (Net35)
            {
                options.ReferencedAssemblies.Add("System.Core.dll");
                options.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            }

            foreach (string reference in referencedAssemblies)
            {
                options.ReferencedAssemblies.Add(reference);
            }

            CompilerResults result = CompileLanguage(sourceFiles, provider, options);

            provider.Dispose();

            return result;
        }

        private static CompilerResults CompileLanguage(string[] sourceFiles, CodeDomProvider provider, CompilerParameters options)
        {
            options.EmbeddedResources.Clear();
            options.LinkedResources.Clear();

            int codeFileCount = 0;
            List<String> files = new List<String>();

            foreach (string file in sourceFiles)
            {
                string ext = Path.GetExtension(file);
                if (String.Compare(ext, "." + provider.FileExtension, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    files.Add(file);
                    codeFileCount++;
                }
            }

            if (codeFileCount > 0)
            {
                try
                {
                    return provider.CompileAssemblyFromFile(options, files.ToArray());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Unexpected error during compilation:\n" + ex.ToString(), "Runtime");
                }
            }

            return null;
        }
    }
}
