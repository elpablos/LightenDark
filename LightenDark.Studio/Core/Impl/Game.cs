using Gemini.Modules.Output;
using Gemini.Modules.PropertyGrid;
using LightenDark.Api;
using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Response;
using LightenDark.Api.Types;
using LightenDark.Module.Console;
using LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core.Impl
{
    [Export(typeof(IGame))]
    public class Game : IGame
    {
        public IPlayer Player { get; set; }

        public IWorld World { get; set; }

        [Import]
        public IOutput Output { get; set; }

        [Import]
        public IBoundClass BoundClass { get; set; }

        private ICefBrowserViewModel browser;
        [Import]
        public ICefBrowserViewModel Browser
        {
            get { return browser; }
            set
            {
                browser = value;
                if (Browser != null) OnBrowserBound();
            }
        }

        [ImportingConstructor]
        public Game(IPropertyGrid propertyGrid)
        {
            propertyGrid.SelectedObject = this;
            Player = new Player(this);
            World = new World(this);
        }

        private void OnBrowserBound()
        {
            BoundClass.BoundMessageHandler += BoundClass_BoundMessageHandler;
        }

        /// <summary>
        /// Resend only incoming message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundClass_BoundMessageHandler(object sender, BoundEventArgs e)
        {
            if (e.BoundType == BoundEnum.In)
            {
                ResponseModelBase responseBase = JsonConvert.DeserializeObject<ResponseModelBase>(e.Message);
                switch (responseBase.Type)
                {
                    case ResponseTypes.Login:
                        var response = JsonConvert.DeserializeObject<ResponseLogin>(e.Message);
                        //Player.ID = resonse.CharacterID;
                        //p
                        break;
                    case ResponseTypes.MobMove:
                        break;
                    case ResponseTypes.CharacterData:
                        break;
                    case ResponseTypes.SendMessage:
                        break;
                    case ResponseTypes.StaticObjectChange:
                        break;
                    case ResponseTypes.ItemDamaged:
                        break;
                    case ResponseTypes.CharactedHpMpChanged:
                        break;
                    case ResponseTypes.CodeBook:
                        break;
                    case ResponseTypes.InventoryChanged:
                        break;
                    case ResponseTypes.ItemAction:
                        break;
                    case ResponseTypes.MapStaticCodeBook:
                        break;
                    case ResponseTypes.SkillSetChanged:
                        break;
                    case ResponseTypes.LoadBank:
                        break;
                    case ResponseTypes.BankOperation:
                        break;
                    case ResponseTypes.CraftList:
                        break;
                    case ResponseTypes.Attack:
                        break;
                    case ResponseTypes.Movement:
                        break;
                    case ResponseTypes.MobData:
                        break;
                    case ResponseTypes.CombatAction:
                        break;
                    case ResponseTypes.LoadLoot:
                        break;
                    case ResponseTypes.LoadOperation:
                        break;
                    case ResponseTypes.OrphanItems:
                        break;
                    case ResponseTypes.PlayerGraves:
                        break;
                    case ResponseTypes.BuyList:
                        break;
                    case ResponseTypes.NpcList:
                        break;
                    case ResponseTypes.ExperienceGoldChanged:
                        break;
                    case ResponseTypes.LevelUp:
                        break;
                    case ResponseTypes.Offer:
                        break;
                    case ResponseTypes.PartyData:
                        break;
                    case ResponseTypes.TradeOperation:
                        break;
                    case ResponseTypes.PowerEnchant:
                        break;
                    case ResponseTypes.Augment:
                        break;
                    case ResponseTypes.ChatMessage:
                        break;
                    case ResponseTypes.MapData:
                        break;
                    case ResponseTypes.CharacterAction:
                        break;
                    case ResponseTypes.NpcData:
                        break;
                    case ResponseTypes.PlayerFraveChanged:
                        break;
                    case ResponseTypes.hunting:
                        break;
                    case ResponseTypes.AffectChange:
                        break;
                    case ResponseTypes.Heal:
                        break;
                    case ResponseTypes.MobDamaged:
                        break;
                    case ResponseTypes.MessageBoardContent:
                        break;
                    case ResponseTypes.NpcChat:
                        break;
                    case ResponseTypes.NpcQuest:
                        break;
                    case ResponseTypes.PlayerInfo:
                        break;
                    case ResponseTypes.GmInit:
                        break;
                    case ResponseTypes.GmAllSpawn:
                        break;
                    case ResponseTypes.GmMessage:
                        break;
                    case ResponseTypes.WorldSave:
                        break;
                    case ResponseTypes.Error:
                        break;
                    case ResponseTypes.MessageBye:
                        break;
                    default:
                        break;
                }

                GameMessage(this, new GameEventArgs(e.Message));
            }
        }

        public event EventHandler<GameEventArgs> GameMessage = delegate { };

        public void SendJavaScript(string message)
        {
            Browser.ExecuteJavaScriptAsync(message);
        }

        public void OutputWrite(string message)
        {
            Output.AppendLine(message);
        }

        #region Game methods

        // {"type":1,"login":"xxxx","password":""}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void Login(string login, string password)
        {

        }

        #endregion
    }
}
