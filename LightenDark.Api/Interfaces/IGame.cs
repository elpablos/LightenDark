using LightenDark.Api.Args;
using LightenDark.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Interfaces
{
    public interface IGame
    {
        /// <summary>
        /// DarkenLight world
        /// </summary>
        IWorld World { get; }

        /// <summary>
        /// DarkenLight player
        /// </summary>
        IPlayer Player { get; }

        /// <summary>
        /// Direct send of JavaScript message 
        /// </summary>
        /// <param name="message"></param>
        [Obsolete]
        void SendJavaScript(string message);

        /// <summary>
        /// Raw messages from server
        /// </summary>
        [Obsolete]
        event EventHandler<GameEventArgs> GameMessage;

        event EventHandler<ResponseAttack> EventAttack;
        event EventHandler<ResponseBankOperation> EventBankOperation;
        event EventHandler<ResponseCastSpell> EventCastSpell;
        event EventHandler<ResponseCharacterAction> EventCharacterAction;
        event EventHandler<ResponseCharacterData> EventCharacterData;
        event EventHandler<ResponseCharacterHpMpChanged> EventCharacterHpMpChanged;
        event EventHandler<ResponseChatMessage> EventChatMessage;
        event EventHandler<ResponseCodeBook> EventCodeBook;
        event EventHandler<ResponseBase> EventCombatAction;
        event EventHandler<ResponseCraftList> EventCraftList;
        event EventHandler<ResponseInventoryChanged> EventInventoryChanged;
        event EventHandler<ResponseItemAction> EventItemAction;
        event EventHandler<ResponseItemDamaged> EventItemDamaged;
        event EventHandler<ResponseLoadBank> EventLoadBank;
        event EventHandler<ResponseLoadLoot> EventLoadLoot;
        event EventHandler<ResponseLogin> EventLogin;
        event EventHandler<ResponseLootOperation> EventLootOperation;
        event EventHandler<ResponseMapData> EventMapData;
        event EventHandler<ResponseMapStaticCodeBook> EventMapStaticCodeBook;
        event EventHandler<ResponseMobData> EventMobData;
        event EventHandler<ResponseMobMove> EventMobMove;
        event EventHandler<ResponseMovement> EventMovement;
        event EventHandler<ResponseNpcData> EventNpcData;
        event EventHandler<ResponseOrphanItems> EventOrphanItems;
        event EventHandler<ResponseSendMessage> EventSendMessage;
        event EventHandler<ResponseSkillSetChanged> EventSkillSetChanged;
        event EventHandler<ResponseStaticObjectChange> EventStaticObjectChange;

        /// <summary>
        /// Write message to console
        /// </summary>
        /// <param name="message"></param>
        void OutputWrite(string message);
        void Login(string login, string password);
    }
}
