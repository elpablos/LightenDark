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
using System.Threading;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core.Impl
{
    [Export(typeof(IGame))]
    public class Game : IGame
    {
        #region Properties

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

        #endregion

        #region Events

        public event EventHandler<GameEventArgs> GameMessage = delegate { };

        public event EventHandler<ResponseLogin> EventLogin = delegate { };

        public event EventHandler<ResponseMobMove> EventMobMove = delegate { };

        public event EventHandler<ResponseCharacterData> EventCharacterData = delegate { };

        public event EventHandler<ResponseSendMessage> EventSendMessage = delegate { };

        public event EventHandler<ResponseStaticObjectChange> EventStaticObjectChange = delegate { };

        public event EventHandler<ResponseItemDamaged> EventItemDamaged = delegate { };

        public event EventHandler<ResponseCharacterHpMpChanged> EventCharacterHpMpChanged = delegate { };

        public event EventHandler<ResponseCodeBook> EventCodeBook = delegate { };

        public event EventHandler<ResponseInventoryChanged> EventInventoryChanged = delegate { };

        public event EventHandler<ResponseItemAction> EventItemAction = delegate { };

        public event EventHandler<ResponseMapStaticCodeBook> EventMapStaticCodeBook = delegate { };

        public event EventHandler<ResponseSkillSetChanged> EventSkillSetChanged = delegate { };

        public event EventHandler<ResponseLoadBank> EventLoadBank = delegate { };

        public event EventHandler<ResponseBankOperation> EventBankOperation = delegate { };

        public event EventHandler<ResponseCraftList> EventCraftList = delegate { };

        public event EventHandler<ResponseAttack> EventAttack = delegate { };

        public event EventHandler<ResponseMovement> EventMovement = delegate { };

        public event EventHandler<ResponseMobData> EventMobData = delegate { };

        public event EventHandler<ResponseBase> EventCombatAction = delegate { };

        public event EventHandler<ResponseLoadLoot> EventLoadLoot = delegate { };

        public event EventHandler<ResponseLootOperation> EventLootOperation = delegate { };

        public event EventHandler<ResponseOrphanItems> EventOrphanItems = delegate { };

        public event EventHandler<ResponseCharacterAction> EventCharacterAction = delegate { };

        public event EventHandler<ResponseMapData> EventMapData = delegate { };

        public event EventHandler<ResponseNpcData> EventNpcData = delegate { };

        public event EventHandler<ResponseChatMessage> EventChatMessage = delegate { };

        public event EventHandler<ResponseCastSpell> EventCastSpell = delegate { };

        #endregion

        #region Constructor

        [ImportingConstructor]
        public Game(IPropertyGrid propertyGrid)
        {
            propertyGrid.SelectedObject = this;
            Player = new Player(this);
            World = new World(this);
        }

        #endregion

        #region Events

        /// <summary>
        /// Resend only incoming message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundClass_BoundMessageHandler(object sender, BoundEventArgs e)
        {
            if (e.BoundType == BoundType.In)
            {
                ResponseBase responseBase = JsonConvert.DeserializeObject<ResponseBase>(e.Message);
                switch (responseBase.ResponseType)
                {
                    case ResponseTypes.Login:
                        var responseLogin = JsonConvert.DeserializeObject<ResponseLogin>(e.Message);
                        EventLogin(this, responseLogin);
                        break;
                    case ResponseTypes.MobMove:
                        var responseMobMove = JsonConvert.DeserializeObject<ResponseMobMove>(e.Message);
                        EventMobMove(this, responseMobMove);
                        break;
                    case ResponseTypes.CharacterData:
                        var responseCharacterData = JsonConvert.DeserializeObject<ResponseCharacterData>(e.Message);
                        EventCharacterData(this, responseCharacterData);
                        break;
                    case ResponseTypes.SendMessage:
                        var responseSendMessage = JsonConvert.DeserializeObject<ResponseSendMessage>(e.Message);
                        EventSendMessage(this, responseSendMessage);
                        break;
                    case ResponseTypes.StaticObjectChange:
                        var responseStaticObjectChange = JsonConvert.DeserializeObject<ResponseStaticObjectChange>(e.Message);
                        EventStaticObjectChange(this, responseStaticObjectChange);
                        break;
                    case ResponseTypes.ItemDamaged:
                        var responseItemDamaged = JsonConvert.DeserializeObject<ResponseItemDamaged>(e.Message);
                        EventItemDamaged(this, responseItemDamaged);
                        break;
                    case ResponseTypes.CharacterHpMpChanged:
                        var responseCharacterHpMpChanged = JsonConvert.DeserializeObject<ResponseCharacterHpMpChanged>(e.Message);
                        EventCharacterHpMpChanged(this, responseCharacterHpMpChanged);
                        break;
                    case ResponseTypes.CodeBook:
                        var responseCodeBook = JsonConvert.DeserializeObject<ResponseCodeBook>(e.Message);
                        EventCodeBook(this, responseCodeBook);
                        break;
                    case ResponseTypes.InventoryChanged:
                        var responseInventoryChanged = JsonConvert.DeserializeObject<ResponseInventoryChanged>(e.Message);
                        EventInventoryChanged(this, responseInventoryChanged);
                        break;
                    case ResponseTypes.ItemAction:
                        var responseItemAction = JsonConvert.DeserializeObject<ResponseItemAction>(e.Message);
                        EventItemAction(this, responseItemAction);
                        break;
                    case ResponseTypes.MapStaticCodeBook:
                        var responseMapStaticCodeBook = JsonConvert.DeserializeObject<ResponseMapStaticCodeBook>(e.Message);
                        EventMapStaticCodeBook(this, responseMapStaticCodeBook);
                        break;
                    case ResponseTypes.SkillSetChanged:
                        var responseSkillSetChanged = JsonConvert.DeserializeObject<ResponseSkillSetChanged>(e.Message);
                        EventSkillSetChanged(this, responseSkillSetChanged);
                        break;
                    case ResponseTypes.LoadBank:
                        var responseLoadBank = JsonConvert.DeserializeObject<ResponseLoadBank>(e.Message);
                        EventLoadBank(this, responseLoadBank);
                        break;
                    case ResponseTypes.BankOperation:
                        var responseBankOperation = JsonConvert.DeserializeObject<ResponseBankOperation>(e.Message);
                        EventBankOperation(this, responseBankOperation);
                        break;
                    case ResponseTypes.CraftList:
                        var responseCraftList = JsonConvert.DeserializeObject<ResponseCraftList>(e.Message);
                        EventCraftList(this, responseCraftList);
                        break;
                    case ResponseTypes.Attack:
                        var responseAttack = JsonConvert.DeserializeObject<ResponseAttack>(e.Message);
                        EventAttack(this, responseAttack);
                        break;
                    case ResponseTypes.Movement:
                        var responseMovement = JsonConvert.DeserializeObject<ResponseMovement>(e.Message);
                        EventMovement(this, responseMovement);
                        break;
                    case ResponseTypes.MobData:
                        var responseMobData = JsonConvert.DeserializeObject<ResponseMobData>(e.Message);
                        EventMobData(this, responseMobData);
                        break;
                    case ResponseTypes.CombatAction:
                        // not implemented in game yet
                        var responseCombatAction = JsonConvert.DeserializeObject<ResponseBase>(e.Message);
                        EventCombatAction(this, responseCombatAction);
                        break;
                    case ResponseTypes.LoadLoot:
                        var responseLoadLoot = JsonConvert.DeserializeObject<ResponseLoadLoot>(e.Message);
                        EventLoadLoot(this, responseLoadLoot);
                        break;
                    case ResponseTypes.LootOperation:
                        var responseLootOperation = JsonConvert.DeserializeObject<ResponseLootOperation>(e.Message);
                        EventLootOperation(this, responseLootOperation);
                        break;
                    case ResponseTypes.OrphanItems:
                        var responseOrphanItems = JsonConvert.DeserializeObject<ResponseOrphanItems>(e.Message);
                        EventOrphanItems(this, responseOrphanItems);
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
                    case ResponseTypes.CastSpell:
                        var responseCastSpell = JsonConvert.DeserializeObject<ResponseCastSpell>(e.Message);
                        EventCastSpell(this, responseCastSpell);
                        break;
                    case ResponseTypes.ChatMessage:
                        var responseChatMessage = JsonConvert.DeserializeObject<ResponseChatMessage>(e.Message);
                        EventChatMessage(this, responseChatMessage);
                        break;
                    case ResponseTypes.MapData:
                        var responseMapData = JsonConvert.DeserializeObject<ResponseMapData>(e.Message);
                        EventMapData(this, responseMapData);
                        break;
                    case ResponseTypes.CharacterAction:
                        var responseCharacterAction = JsonConvert.DeserializeObject<ResponseCharacterAction>(e.Message);
                        EventCharacterAction(this, responseCharacterAction);
                        break;
                    case ResponseTypes.NpcData:
                        var responseNpcData = JsonConvert.DeserializeObject<ResponseNpcData>(e.Message);
                        EventNpcData(this, responseNpcData);
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

        #endregion

        #region Public methods

        public void SendJavaScript(string message)
        {
            OutputWrite("JS: "+ message);
            Browser.ExecuteJavaScriptAsync(message);
        }

        public void OutputWrite(string message)
        {
            Output.AppendLine(message);
        }

        #endregion

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

        #region Private methods

        private void OnBrowserBound()
        {
            BoundClass.BoundMessageHandler += BoundClass_BoundMessageHandler;
        }

        #endregion

        #region ResponseLoop

        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public CancellationToken CancelToken { get { return CancelTokenSource.Token; } }

        public CancellationTokenSource CancelTokenSource { get { return cancelTokenSource; } }

        public async Task<T> ResponseWaitBase<T>(
            Action body,
            EventHandler<T> responseBeforeHandler, 
            string eventName, 
            int timeout = Timeout.Infinite)
        where T : ResponseBase
        {
            var tcs = new TaskCompletionSource<T>();

            // event methods
            var eventInfo = this.GetType().GetEvent(eventName);
            System.Reflection.MethodInfo eventAdd = eventInfo.GetAddMethod();
            System.Reflection.MethodInfo eventRem = eventInfo.GetRemoveMethod();

            // prepare the timeout
            CancellationToken newToken;
            if (timeout != Timeout.Infinite)
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(CancelToken);
                cts.CancelAfter(timeout);
                newToken = cts.Token;
            }
            else
                newToken = CancelToken;

            // register cancelation
            newToken.Register(() => tcs.SetCanceled(), useSynchronizationContext: false);
            EventHandler<T> handler = null;
            handler = (s, e) =>
            {
                if (e.Cancelled)
                {
                    // while cancel
                    tcs.TrySetCanceled();
                }
                else if (e.Error != null)
                {
                    // catch errors
                    tcs.SetException(e.Error);
                }
                else
                {
                    // do some actions before return data
                    if (responseBeforeHandler != null)
                       responseBeforeHandler(s, e);
                    tcs.SetResult(e);
                }
                eventRem.Invoke(this, new object[] { handler });
            };
            
            eventAdd.Invoke(this, new object[] { handler });
            await Task.Delay(500); // needed delay! 

            body();

            return await tcs.Task;
        }

        #endregion
    }
}
