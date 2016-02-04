using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Types
{
    /// <summary>
    /// Types of requests
    /// </summary>
    public enum RequestTypes
    {
        Login = 1,
        AutoAttack = 5,
        SendMessage = 6,
        CodeBook = 11,
        ItemUse = 13,
        ItemAct = 15,
        ActionBasic = 20,
        LoadBank = 21,
        BankOperation = 23,
        ActionProcess = 25,
        CraftList = 26,
        ActionCraft = 28,
        Movement = 31,
        CombatAction = 34,
        LoadLoot = 36,
        LoopOperation = 38,
        BuyList = 42,
        NpcBuy = 44,
        PartyAction = 48,
        OfferReaction = 50,
        TradeOperation = 52,
        PowerEnchant = 54,
        Augment = 56,
        ActionCamp = 58,
        Spell = 59,
        Stop = 61,
        PickUp = 62,
        MapData = 64,
        Gather = 66,
        NpcHealerBuy = 69,
        OpenGrave = 70,
        LootFromGrave = 71,
        Hunting = 73,
        Campfire = 76,
        Remedy = 78,
        MinionCommand = 80,
        UseStatic = 81,
        SpecialSkill = 83,
        NpcChat = 84,
        NpcQuest = 86,
        NpcSell = 88,
        PlayerInfo = 89,
        GmInit = 500,
        GmStaticOp=502,
        GmDeleteSpawn = 503,
        GmCreateSpawn = 504,
        GmMessage = 506,
        GmWorldSave = 508,
        GmTeleport = 509
    }
}
