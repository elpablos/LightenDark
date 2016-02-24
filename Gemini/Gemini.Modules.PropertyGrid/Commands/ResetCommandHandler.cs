using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using Gemini.Modules.PropertyGrid.ViewModels;

namespace Gemini.Modules.PropertyGrid.Commands
{
    [CommandHandler]
    public class ResetCommandHandler : CommandHandlerBase<ResetCommandDefinition>
    {
        private readonly IPropertyGridExtended _propertyGrid;

        [ImportingConstructor]
        public ResetCommandHandler(IPropertyGridExtended propertyGrid)
        {
            _propertyGrid = propertyGrid;
        }

        public override Task Run(Command command)
        {
            _propertyGrid.Reset();
            return TaskUtility.Completed;
        }
    }
}