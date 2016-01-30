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
                ResponseBase responseBase = JsonConvert.DeserializeObject<ResponseBase>(e.Message);
                switch (responseBase.ResponseType)
                {
                    case ResponseTypes.Login:
                        var responseLogin = JsonConvert.DeserializeObject<ResponseLogin>(e.Message);
                        break;
                    case ResponseTypes.MobMove:
                        var responseMobMove = JsonConvert.DeserializeObject<ResponseMobMove>(e.Message);
                        break;
                    case ResponseTypes.CharacterData:
                        var responseCharacterData = JsonConvert.DeserializeObject<ResponseCharacterData>(e.Message);
                        break;
                    case ResponseTypes.SendMessage:
                        var responseSendMessage = JsonConvert.DeserializeObject<ResponseSendMessage>(e.Message);
                        break;
                    case ResponseTypes.StaticObjectChange:
                        var responseStaticObjectChange = JsonConvert.DeserializeObject<ResponseStaticObjectChange>(e.Message);
                        break;
                    case ResponseTypes.ItemDamaged:
                        var responseItemDamaged = JsonConvert.DeserializeObject<ResponseItemDamaged>(e.Message);
                        break;
                    case ResponseTypes.CharacterHpMpChanged:
                        var responseCharacterHpMpChanged= JsonConvert.DeserializeObject<ResponseCharacterHpMpChanged>(e.Message);
                        break;
                    case ResponseTypes.CodeBook:
                        // TODO nedodelane!
                        var responseCodeBook = JsonConvert.DeserializeObject<ResponseCodeBook>(e.Message);
                        break;
                    case ResponseTypes.InventoryChanged:
                        var responseInventoryChanged= JsonConvert.DeserializeObject<ResponseInventoryChanged>(e.Message);
                        break;
                    case ResponseTypes.ItemAction:
                        var responseItemAction = JsonConvert.DeserializeObject<ResponseItemAction>(e.Message);
                        break;
                    case ResponseTypes.MapStaticCodeBook:
                        var responseMapStaticCodeBook = JsonConvert.DeserializeObject<ResponseMapStaticCodeBook>(e.Message);
                        break;
                    case ResponseTypes.SkillSetChanged:
                        var responseSkillSetChanged = JsonConvert.DeserializeObject<ResponseSkillSetChanged>(e.Message);
                        break;
                    case ResponseTypes.LoadBank:
                        var responseLoadBank = JsonConvert.DeserializeObject<ResponseLoadBank>(e.Message);
                        break;
                    case ResponseTypes.BankOperation:
                        var responseBankOperation = JsonConvert.DeserializeObject<ResponseBankOperation>(e.Message);
                        break;
                    case ResponseTypes.CraftList:
                        var responseCraftList = JsonConvert.DeserializeObject<ResponseCraftList>(e.Message);
                        break;
                    case ResponseTypes.Attack:
                        // TODO nedodelane!
                        var responseAttack = JsonConvert.DeserializeObject<ResponseAttack>(e.Message);
                        break;
                    case ResponseTypes.Movement:
                        var responseMovement = JsonConvert.DeserializeObject<ResponseMovement>(e.Message);
                        break;
                    case ResponseTypes.MobData:
                        var ResponseMobData = JsonConvert.DeserializeObject<ResponseMobData>(e.Message);
                        break;
                    case ResponseTypes.CombatAction:
                        // not implemented in game yet
                        var responseCombatAction = JsonConvert.DeserializeObject<ResponseBase>(e.Message);
                        break;
                    case ResponseTypes.LoadLoot:
                        var responseLoadLoot = JsonConvert.DeserializeObject<ResponseLoadLoot>(e.Message);
                        break;
                    case ResponseTypes.LootOperation:
                        var responseLootOperation = JsonConvert.DeserializeObject<ResponseLootOperation>(e.Message);
                        break;
                    case ResponseTypes.OrphanItems:
                        var responseOrphanItems = JsonConvert.DeserializeObject<ResponseOrphanItems>(e.Message);
                        break;
                    case ResponseTypes.PlayerGraves:
                        var responsePlayerGraves = JsonConvert.DeserializeObject<ResponsePlayerGraves>(e.Message);
                        break;
                    case ResponseTypes.BuyList:
                        var responseBuyList = JsonConvert.DeserializeObject<ResponseBuyList>(e.Message);
                        break;
                    case ResponseTypes.NpcBuy:
                        var responseNpcBuy = JsonConvert.DeserializeObject<ResponseNpcBuy>(e.Message);
                        break;
                    case ResponseTypes.ExperienceGoldChanged:
                        var responseExperienceGoldChanged = JsonConvert.DeserializeObject<ResponseExperienceGoldChanged>(e.Message);
                        break;
                    case ResponseTypes.LevelUp:
                            var responseLevelUp = JsonConvert.DeserializeObject<ResponseLevelUp>(e.Message);
                        break;
                    case ResponseTypes.Offer:
                        var responseOffer = JsonConvert.DeserializeObject<ResponseOffer>(e.Message);
                        break;
                    case ResponseTypes.PartyData:
                        var responsePartyData = JsonConvert.DeserializeObject<ResponsePartyData>(e.Message);
                        break;
                    case ResponseTypes.TradeOperation:
                        var responseTradeOperation = JsonConvert.DeserializeObject<ResponseTradeOperation>(e.Message);
                        break;
                    case ResponseTypes.PowerEnchant:
                        var responsePowerEnchant = JsonConvert.DeserializeObject<ResponsePowerEnchant>(e.Message);
                        break;
                    case ResponseTypes.Augment:
                        var responseAugment = JsonConvert.DeserializeObject<ResponseAugment>(e.Message);
                        break;
                    case ResponseTypes.ChatMessage:
                        var responseChatMessage = JsonConvert.DeserializeObject<ResponseChatMessage>(e.Message);
                        break;
                    case ResponseTypes.MapData:
                        var responseMapData = JsonConvert.DeserializeObject<ResponseMapData>(e.Message);
                        break;
                    case ResponseTypes.CharacterAction:
                        var responseCharacterAction = JsonConvert.DeserializeObject<ResponseCharacterAction>(e.Message);
                        break;
                    case ResponseTypes.NpcData:
                        var responseNpcData = JsonConvert.DeserializeObject<ResponseNpcData>(e.Message);
                        break;
                    case ResponseTypes.PlayerGraveChanged:
                        var responsePlayerGraveChanged = JsonConvert.DeserializeObject<ResponsePlayerGraveChanged>(e.Message);
                        break;
                    case ResponseTypes.Hunting:
                        var responseHunting = JsonConvert.DeserializeObject<ResponseHunting>(e.Message);
                        break;
                    case ResponseTypes.AffectChange:
                        var responseAffectChange = JsonConvert.DeserializeObject<ResponseAffectChange>(e.Message);
                        break;
                    case ResponseTypes.Heal:
                        var responseHeal = JsonConvert.DeserializeObject<ResponseHeal>(e.Message);
                        break;
                    case ResponseTypes.MobDamaged:
                        var responseMobDamaged = JsonConvert.DeserializeObject<ResponseMobDamaged>(e.Message);
                        break;
                    case ResponseTypes.MessageBoardContent:
                        var responseMessageBoardContent = JsonConvert.DeserializeObject<ResponseMessageBoardContent>(e.Message);
                        break;
                    case ResponseTypes.NpcChat:
                        var responseNpcChat = JsonConvert.DeserializeObject<ResponseNpcChat>(e.Message);
                        break;
                    case ResponseTypes.NpcQuest:
                        var responseNpcQuest = JsonConvert.DeserializeObject<ResponseNpcQuest>(e.Message);
                        break;
                    case ResponseTypes.PlayerInfo:
                        var responsePlayerInfo = JsonConvert.DeserializeObject<ResponsePlayerInfo>(e.Message);
                        break;
                    case ResponseTypes.GmInit:
                        var responseGmInit = JsonConvert.DeserializeObject<ResponseGmInit>(e.Message);
                        break;
                    case ResponseTypes.GmAllSpawn:
                        var responseGmAllSpawn = JsonConvert.DeserializeObject<ResponseGmAllSpawn>(e.Message);
                        break;
                    case ResponseTypes.GmMessage:
                        var responseGmMessage = JsonConvert.DeserializeObject<ResponseGmMessage>(e.Message);
                        break;
                    case ResponseTypes.WorldSave:
                        var responseWorldSave = JsonConvert.DeserializeObject<ResponseWorldSave>(e.Message);
                        break;
                    case ResponseTypes.Error:
                        // not implemented in game yet
                        var responseError = JsonConvert.DeserializeObject<ResponseBase>(e.Message);
                        break;
                    case ResponseTypes.ByeMessage:
                        var responseByeMessage = JsonConvert.DeserializeObject<ResponseByeMessage>(e.Message);
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
