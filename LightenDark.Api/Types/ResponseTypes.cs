namespace LightenDark.Api.Types
{
    /// <summary>
    /// Všechny typy odpovědí ze serveru
    /// </summary>
    public enum ResponseTypes
    {
        /// <summary>
        /// Odpoved na pozadavek na pridani postavy do hry
        /// Vychozi nastaveni sveta, postavy, inventare, skillu atd.
        /// <see cref="Response.ResponseLogin"/>
        /// </summary>
        Login = 2,

        /// <summary>
        /// Zprava s pohybem moba
        /// <see cref="Response.ResponseMobMove"/>
        /// </summary>
        MobMove = 3,

        /// <summary>
        /// Zprava s daty o cizi postave
        /// <see cref="Response.ResponseCharacterData"/>
        /// </summary>
        CharacterData = 4,

        /// <summary>
        /// Odpoved se zpravou
        /// <see cref="Response.ResponseSendMessage"/>
        /// </summary>
        SendMessage = 7,

        /// <summary>
        /// Zmena statickeho objektu
        /// <see cref="Response.ResponseStaticObjectChange"/>
        /// </summary>
        StaticObjectChange = 8,

        /// <summary>
        /// Kdyz byl poskozen item
        /// <see cref="Response.ResponseItemDamaged"/>
        /// </summary>
        ItemDamaged = 9,

        /// <summary>
        /// Zmena HP / MP postavy
        /// <see cref="Response.ResponseCharacterHpMpChanged"/>
        /// </summary>
        CharacterHpMpChanged = 10,

        /// <summary>
        /// Odpoved na pozadavek na kodebooky - obsahuje sezamy vsech kategorii, plny je
        /// ale vzdy jen jeden - 1 kategorie = 1 request
        /// <see cref="Response.ResponseCodeBook"/>
        /// </summary>
        CodeBook = 12,

        /// <summary>
        /// Zmena dat inventare
        /// <see cref="Response.ResponseInventoryChanged"/>
        /// </summary>
        InventoryChanged = 14,

        /// <summary>
        /// Obecna odpoved na pozadavek na akci s predmetem
        /// <see cref="Response.ResponseItemAction"/>
        /// </summary>
        ItemAction = 16,

        /// <summary>
        /// Číselník statických položek?
        /// <see cref="Response.ResponseMapStaticCodeBook"/>
        /// </summary>
        MapStaticCodeBook = 17,

        /// <summary>
        /// Změna ve skillsetu
        /// <see cref="Response.ResponseSkillSetChanged"/>
        /// </summary>
        SkillSetChanged = 18,

        /// <summary>
        /// Mačtení dat banky
        /// <see cref="Response.ResponseLoadBank"/>
        /// </summary>
        LoadBank = 22,

        /// <summary>
        ///  Operace v bance vyber/vklad
        ///  <see cref="Response.ResponseBankOperation"/>
        /// </summary>
        BankOperation = 24,

        /// <summary>
        ///  Odpoved na pozadavek na seznam pro crafting
        ///  <see cref="Response.ResponseCraftList"/>
        /// </summary>
        CraftList = 27,

        /// <summary>
        /// Odpoved na pozadavek na utok
        /// <see cref="Response.ResponseAttack"/>
        /// </summary>
        Attack = 30,

        /// <summary>
        /// Odpoved na pozadavek na pohyb
        /// <see cref="Response.ResponseMovement"/>
        /// </summary>
        Movement = 32,

        /// <summary>
        /// Zprava s daty o mobovi
        /// <see cref="Response.ResponseMobData"/>
        /// </summary>
        MobData = 33,

        /// <summary>
        /// Not implemented in game yet
        /// </summary>
        CombatAction = 35,

        /// <summary>
        /// Odpoved na pozadavek na load lootu
        /// <see cref="Response.ResponseLoadLoot"/>
        /// </summary>
        LoadLoot = 37,

        /// <summary>
        /// Pozadavek na operaci s lootem
        /// <see cref="Response.ResponseLootOperation"/>
        /// </summary>
        LootOperation = 39,

        /// <summary>
        /// Odpoved na itemy na zemi
        /// <see cref="Response.ResponseOrphanItems"/>
        /// </summary>
        OrphanItems = 40,

        /// <summary>
        /// Odpoved na hrobecek
        /// <see cref="Response.ResponsePlayerGraves"/>
        /// </summary>
        PlayerGraves = 41,

        /// <summary>
        /// Odpoved na pozadavek na seznam pro nakupovani
        /// <see cref="Response.ResponseBuyList"/>
        /// </summary>
        BuyList = 43,

        /// <summary>
        /// Odpoved na pozadavek na nakup itemu od NPC
        /// <see cref="Response.ResponseNpcBuy"/>
        /// </summary>
        NpcBuy = 45,

        /// <summary>
        /// Zmena Exp/gold - pri zabiti moba
        /// <see cref="Response.ResponseExperienceGoldChanged"/>
        /// </summary>
        ExperienceGoldChanged = 46,

        /// <summary>
        /// Odpoved na pozadavek na pridani levelu
        /// <see cref="Response.ResponseLevelUp"/>
        /// </summary>
        LevelUp = 47,

        /// <summary>
        /// Zprava s nabidkou co je casove omezena
        /// <see cref="Response.ResponseOffer"/>
        /// </summary>
        Offer = 49,

        /// <summary>
        /// Zprava s daty party
        /// <see cref="Response.ResponsePartyData"/>
        /// </summary>
        PartyData = 51,

        /// <summary>
        /// Odpoved na pozadavek na operaci v trade
        /// <see cref="Response.ResponseTradeOperation"/>
        /// </summary>
        TradeOperation = 53,

        /// <summary>
        /// Enchanty
        /// <see cref="Response.ResponsePowerEnchant"/>
        /// </summary>
        PowerEnchant = 55,

        /// <summary>
        /// Augment
        /// <see cref="Response.ResponseAugment"/>
        /// </summary>
        Augment = 57,

        /// <summary>
        /// Obecna odpoved na pozadavek na akci, obsahuje informativni model akce
        /// <see cref="Response.ResponseCastSpell"/>
        /// </summary>
        CastSpell = 60,

        /// <summary>
        /// Zprava v chatu
        /// <see cref="Response.ResponseChatMessage"/>
        /// </summary>
        ChatMessage = 63,

        /// <summary>
        /// Odpoved na data mapy
        /// <see cref="Response.ResponseMapData"/>
        /// </summary>
        MapData = 65,

        /// <summary>
        /// Odpoved s nazvem akce co postava vykonava
        /// <see cref="Response.ResponseCharacterAction"/>
        /// </summary>
        CharacterAction = 67,

        /// <summary>
        /// Zprava s daty o NPC
        /// <see cref="Response.ResponseNpcData"/>
        /// </summary>
        NpcData = 68,

        /// <summary>
        ///  Zprava s updatem hrobecku
        ///  <see cref="Response.ResponsePlayerGraveChanged"/>
        /// </summary>
        PlayerGraveChanged = 72,

        /// <summary>
        /// Response na pozadavek na hunting
        /// <see cref="Response.ResponseHunting"/>
        /// </summary>
        Hunting = 74,

        /// <summary>
        /// Zmena afektu postavy
        /// <see cref="Response.ResponseAffectChange"/>
        /// </summary>
        AffectChange = 75,

        /// <summary>
        /// Response na healing - lahvi/bandagi/kouzlem
        /// <see cref="Response.ResponseHeal"/>
        /// </summary>
        Heal = 77,

        /// <summary>
        /// Zprava s poskozenim moba - treba DOTkou
        /// <see cref="Response.ResponseMobDamaged"/>
        /// </summary>
        MobDamaged = 79,

        /// <summary>
        /// Odpoved se zpravu message boardu
        /// <see cref="Response.ResponseMessageBoardContent"/>
        /// </summary>
        MessageBoardContent = 82,

        /// <summary>
        /// Chat s NPC
        /// <see cref="Response.ResponseNpcChat"/>
        /// </summary>
        NpcChat = 85,

        /// <summary>
        /// Odpoved na operaci s questem
        /// <see cref="Response.ResponseNpcQuest"/>
        /// </summary>
        NpcQuest = 87,

        /// <summary>
        /// Změna informaci o hraci
        /// <see cref="Response.ResponsePlayerInfo"/>
        /// </summary>
        PlayerInfo = 90,

        /// <summary>
        /// Odpoved na pozadavek na pridani postavy do hry
        /// <see cref="Response.ResponseGmInit"/>
        /// </summary>
        GmInit = 501,

        /// <summary>
        /// Odesle GMku data o vsech spawnech
        /// <see cref="Response.ResponseGmAllSpawn"/>
        /// </summary>
        GmAllSpawn = 505,

        /// <summary>
        /// Zprava od GM
        ///  <see cref="Response.ResponseGmMessage"/>
        /// </summary>
        GmMessage = 507,

        /// <summary>
        /// Odpoved na request rikajici, ze je worldsave
        /// <see cref="Response.ResponseWorldSave"/>
        /// </summary>
        WorldSave = 999,

        /// <summary>
        /// Not implemented in game yet
        /// </summary>
        Error = 1000,

        /// <summary>
        ///  Zprava o ukonceni komunikace
        /// <see cref="Response.ResponseByeMessage"/>
        /// </summary>
        ByeMessage = 0
    }
}
