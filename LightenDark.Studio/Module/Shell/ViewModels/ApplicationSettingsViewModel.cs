using Caliburn.Micro;
using Gemini.Modules.Settings;
using LightenDark.Studio.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.Shell.ViewModels
{
    /// <summary>
    /// Additional settings in application
    /// </summary>
    [Export(typeof(ISettingsEditor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApplicationSettingsViewModel : PropertyChangedBase, ISettingsEditor
    {
        private bool _confirmExit;
        private string _gameUrl;

        public ApplicationSettingsViewModel()
        {
            ConfirmExit = Settings.Default.ConfirmExit;
            GameUrl = Settings.Default.GameUrl;
        }

        public bool ConfirmExit
        {
            get { return _confirmExit; }
            set
            {
                if (value.Equals(_confirmExit)) return;
                _confirmExit = value;
                NotifyOfPropertyChange(() => ConfirmExit);
            }
        }

        public string GameUrl
        {
            get { return _gameUrl; }
            set
            {
                if (value.Equals(_gameUrl)) return;
                _gameUrl = value;
                NotifyOfPropertyChange(() => GameUrl);
            }
        }

        public string SettingsPageName
        {
            get { return "General"; }
        }

        public string SettingsPagePath
        {
            get { return "Environment"; }
        }

        public void ApplyChanges()
        {
            if (ConfirmExit == Settings.Default.ConfirmExit
                || GameUrl == Settings.Default.GameUrl)
            {
                return;
            }

            Settings.Default.GameUrl = GameUrl;
            Settings.Default.ConfirmExit = ConfirmExit;

            // save
            Settings.Default.Save();
        }
    }
}
