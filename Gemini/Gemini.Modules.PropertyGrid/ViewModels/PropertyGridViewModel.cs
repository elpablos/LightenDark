using System;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Modules.UndoRedo.Commands;
using Gemini.Modules.PropertyGrid.Commands;
using System.Threading.Tasks;
using Gemini.Framework.Threading;

namespace Gemini.Modules.PropertyGrid.ViewModels
{
    public interface IPropertyGridExtended : IPropertyGrid
    {
        void Reset();
    }

	[Export(typeof(IPropertyGrid))]
    [Export(typeof(IPropertyGridExtended))]
    public class PropertyGridViewModel : Tool, IPropertyGrid, IPropertyGridExtended, ICommandRerouter // , ICommandHandler<ResetCommandDefinition>
	{
	    private readonly IShell _shell;

		public override PaneLocation PreferredLocation
		{
			get { return PaneLocation.Right; }
		}

		public override Uri IconSource
		{
			get { return new Uri("pack://application:,,,/Gemini.Modules.PropertyGrid;component/Resources/Icons/Properties.png"); }
		}

        private object _defaultSelectedObject;

        private object _selectedObject;
		public object SelectedObject
		{
			get { return _selectedObject; }
			set
			{
				_selectedObject = value;
                _defaultSelectedObject = value;
                NotifyOfPropertyChange(() => SelectedObject);
			}
		}

        private string _selectedProperty;
        public string SelectedProperty
        {
            get { return _selectedProperty; }
            set
            {
                if (_selectedProperty != value && value != null)
                {
                    var newobject = _selectedObject.GetType().GetProperty(value).GetValue(_selectedObject, null);
                    if (newobject != null && newobject.GetType().Namespace.StartsWith("LightenDark"))
                    {
                        _selectedObject = newobject;
                        NotifyOfPropertyChange(() => SelectedObject);
                    }
                }

                _selectedProperty = value;
                NotifyOfPropertyChange(() => SelectedProperty);
            }
        }

        [ImportingConstructor]
        public PropertyGridViewModel(IShell shell)
        {
            _shell = shell;
            DisplayName = "Properties";

            ToolBarDefinition = ToolBarDefinitions.PropertiesToolBar;
        }

	    object ICommandRerouter.GetHandler(CommandDefinitionBase commandDefinition)
	    {
	        if (commandDefinition is UndoCommandDefinition ||
	            commandDefinition is RedoCommandDefinition)
	        {
	            return _shell.ActiveItem;
	        }

	        return null;
	    }

        #region ResetCommandDefinition

        public void Reset()
        {
            _selectedObject = _defaultSelectedObject;
            NotifyOfPropertyChange(() => SelectedObject);
        }

        //void ICommandHandler<ResetCommandDefinition>.Update(Command command)
        //{
        //    command.Enabled = true;
        //    command.Checked = true;
        //}

        //Task ICommandHandler<ResetCommandDefinition>.Run(Command command)
        //{

        //    return TaskUtility.Completed;
        //}

        #endregion


    }
}