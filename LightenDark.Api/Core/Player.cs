using Caliburn.Micro;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Models;
using LightenDark.Api.Response;
using LightenDark.Api.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightenDark.Api.Core
{
    /// <summary>
    /// Implementace objektu reprezentujici hrace
    /// </summary>
    [Export(typeof(IPlayer))]
    public class Player : PropertyChangedBase, IPlayer
    {
        #region Fields

        private IGame game;

        #endregion

        #region Properties

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyOfPropertyChange(() => ID);
            }
        }

        private GameCharacterModel gameCharacter;
        public GameCharacterModel GameCharacter
        {
            get { return gameCharacter; }
            set
            {
                gameCharacter = value;
                NotifyOfPropertyChange(() => GameCharacter);
            }
        }

        private GameCharacterDataModel gameCharacterData;
        public GameCharacterDataModel GameCharacterData
        {
            get { return gameCharacterData; }
            set
            {
                gameCharacterData = value;
                NotifyOfPropertyChange(() => GameCharacterData);
            }
        }

        public InventoryModel Inventory { get; private set; }
        public EquipSetModel EquipmentSet { get; private set; }
        public SkillSetModel SkillSet { get; private set; }

        public short Xpos { get; private set; }
        public short Ypos { get; private set; }
        public int Mp { get; private set; }
        public int Hp { get; private set; }
        public byte LawStatus { get; private set; }

        public TileTypes TileType { get; private set; }

        [Import]
        public IGame Game
        {
            get { return game; }
            set
            {
                game = value;
                if (game != null)
                {
                    GameBounded();
                }
            }
        }

        private void GameBounded()
        {
            Game.EventLogin += Game_EventLogin;
            Game.EventCharacterData += Game_EventCharacterData;
            Game.EventCharacterHpMpChanged += Game_EventCharacterHpMpChanged;
            Game.EventItemDamaged += Game_EventItemDamaged;
            Game.EventInventoryChanged += Game_EventInventoryChanged;
            Game.EventSkillSetChanged += Game_EventSkillSetChanged;
            Game.EventMovement += Game_EventMovement;
        }

        #endregion

        #region Constructor


        #endregion

        #region Events

        private void Game_EventMovement(object sender, Api.Response.ResponseMovement e)
        {
            if (e.ID == ID)
            {
                Xpos = e.XPos;
                Ypos = e.YPos;
                TileType = (TileTypes)game.World.WorldMap[Xpos][Ypos];
            }
        }

        private void Game_EventSkillSetChanged(object sender, Api.Response.ResponseSkillSetChanged e)
        {
            SkillSet = e.Skillset;
            GameCharacterData = e.GameCharacterData;
            GameCharacter = e.GameCharacter;
        }

        private void Game_EventInventoryChanged(object sender, Api.Response.ResponseInventoryChanged e)
        {
            if (e.ArmorAdded != null)
                Inventory.Armors.AddRange(e.ArmorAdded);
            if (e.ArmorRemoved != null)
                Inventory.Armors.RemoveAll(x => e.ArmorRemoved.Contains(x));

            if (e.WeaponAdded != null)
                Inventory.Weapons.AddRange(e.WeaponAdded);
            if (e.WeaponRemoved != null)
                Inventory.Weapons.RemoveAll(x => e.WeaponRemoved.Contains(x));

            if (e.MaterialAdded != null)
                Inventory.Materials.AddRange(e.MaterialAdded);
            if (e.MaterialRemoved != null)
                Inventory.Materials.RemoveAll(x => e.MaterialRemoved.Contains(x));

            if (e.JewelAdded != null)
                Inventory.Jewels.AddRange(e.JewelAdded);
            if (e.JewelRemoved != null)
                Inventory.Jewels.RemoveAll(x => e.JewelRemoved.Contains(x));

            if (e.EquipSet != null)
                EquipmentSet = e.EquipSet;
            if (e.GameCharacter != null)
                GameCharacter = e.GameCharacter;
            if (e.GameCharacterData != null)
                GameCharacterData = e.GameCharacterData;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_EventItemDamaged(object sender, Api.Response.ResponseItemDamaged e)
        {
            // zbran
            if (e.ItemType == 1)
            {
                if (EquipmentSet.WeaponLeftHand.ID == e.ID)
                {
                    EquipmentSet.WeaponLeftHand.Durability = e.Durability;
                    EquipmentSet.WeaponLeftHand.MaximumDurability = e.MaximumDurability;
                }
            }
        }

        private void Game_EventCharacterHpMpChanged(object sender, Api.Response.ResponseCharacterHpMpChanged e)
        {
            if (e.ID == ID)
            {
                Hp = e.Hp;
                Mp = e.Mp;
                LawStatus = e.LawStatus;
                // e.DOTDamage;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_EventCharacterData(object sender, Api.Response.ResponseCharacterData e)
        {
            if (e.ID == ID)
            {
                Xpos = e.XPos;
                Ypos = e.YPos;
                Hp = e.Hp;
            }

        }

        private void Game_EventLogin(object sender, Api.Response.ResponseLogin e)
        {
            ID = e.CharacterID;
            Hp = e.GameCharacter.HitPoints;
            Mp = e.GameCharacter.Mana;
            LawStatus = e.GameCharacter.LawStatus;
            GameCharacter = e.GameCharacter;
            GameCharacterData = e.GameCharacterData;
            Inventory = e.Inventory;
            EquipmentSet = e.EquipmentSet;
            SkillSet = e.Skillset;
            Xpos = e.Xpos;
            Ypos = e.Ypos;
            DisplayName = GameCharacter.FirstName + " " + GameCharacter.SurName;
        }

        #endregion

        #region Public methods

        public void AutoAttack(int? mobID, int? characterID)
        {
            game.SendJavaScript(string.Format("requestAutoAttack({0}, {1});", mobID, characterID));
        }

        public void Remedy(int characterID)
        {
            game.SendJavaScript(string.Format("requestRemedy({0});", characterID));
        }

        public void SendMessage(string text, string type)
        {
            game.SendJavaScript(string.Format("requestSendMessage({0}, {1});", text, type));
        }

        public void ItemUse(int itemID, string itemCode)
        {
            game.SendJavaScript(string.Format("requestItemUse({0}, {1});", itemID, itemCode));
        }

        public void Hunting(int mobID)
        {
            game.SendJavaScript(string.Format("requestHunting({0});", mobID));
        }

        public void Meditation()
        {
            game.SendJavaScript(string.Format("requestMeditation();"));
        }

        public void CastSpell(int spell)
        {
            game.SendJavaScript(string.Format("requestCastSpell({0});", spell));
        }

        #region Movement

        public Task<ResponseMovement> MoveRightAsync(int timeout = Timeout.Infinite)
        {
            return MovementBaseAsync(MoveRight, timeout);
        }

        public Task<ResponseMovement> MoveLeftAsync(int timeout = Timeout.Infinite)
        {
            return MovementBaseAsync(MoveLeft, timeout);
        }

        public Task<ResponseMovement> MoveDownAsync(int timeout = Timeout.Infinite)
        {
            return MovementBaseAsync(MoveDown, timeout);
        }

        public Task<ResponseMovement> MoveUpAsync(int timeout = Timeout.Infinite)
        {
            return MovementBaseAsync(MoveUp, timeout);
        }

        private Task<ResponseMovement> MovementBaseAsync(System.Action action, int timeout = Timeout.Infinite)
        {
            return game.ResponseWaitBase<ResponseMovement>(action,
                async (s, e) =>
                {
                    await Task.Delay(GameCharacter.MoveTime);
                }, "EventMovement", timeout);
        }


        public void MoveDown()
        {
            game.SendJavaScript("moveDown();");
        }

        private void MoveLeft()
        {
            game.SendJavaScript("moveLeft();");
        }

        private void MoveRight()
        {
            game.SendJavaScript("moveRight();");
        }

        private void MoveUp()
        {
            game.SendJavaScript("moveUp();");
        }

        #endregion

        #region Actions

        public void Stop()
        {
            game.SendJavaScript("requestStop();");
        }

        public void ActionBasic(ActionBasicType type)
        {
            ActionBasic((int)type);
        }

        public void Garthering(GartheringType type)
        {
            Garthering((int)type);
        }

        public void Specialskill(SpecialskillType type)
        {
            Specialskill((int)type);
        }

        public void Garthering(int type)
        {
            string js = string.Format("ws.send('{{\"type\":66,\"gatherType\":{0}}}');", type);
            game.SendJavaScript(js);
        }

        public void Specialskill(int type)
        {
            string js = string.Format("ws.send('{{\"type\":83,\"skillType\":{0}}}');", type);
            game.SendJavaScript(js);
        }

        public void ActionBasic(int type)
        {
            string js = string.Format("ws.send('{{\"type\":20,\"actType\":{0}}}');", type);
            game.SendJavaScript(js);
        }

        public Task<ResponseCastSpell> CastSpellAsync(int chrTgt, int mobTgt, int spell, int timeout = Timeout.Infinite)
        {
            // wait for response
            return game.ResponseWaitBase<ResponseCastSpell>(
                () =>
                {
                    // cast spell
                    string js = string.Format("ws.send('{{\"type\":59,\"chrTgt\":{0},\"mobTgt\":{1},\"spell\":{2}}}');", chrTgt, mobTgt, spell);
                    game.SendJavaScript(js);
                },
                async (s, e) =>
                {
                    // wait for casting time
                    await Task.Delay((int)e.CastingTime, game.CancelTokenSource.Token);
                }, "EventCastSpell", timeout);
        }

        #endregion

        #endregion
    }
}
