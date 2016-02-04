using Caliburn.Micro;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core.Impl
{
    [Export(typeof(IPlayer))]
    public class Player : PropertyChangedBase, IPlayer
    {
        #region Fields

        private Game game;

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

        #endregion

        #region Constructor

        public Player(Game game)
        {
            this.game = game;
            game.EventLogin += Game_EventLogin;
            game.EventCharacterData += Game_EventCharacterData;
            game.EventCharacterHpMpChanged += Game_EventCharacterHpMpChanged;
            game.EventItemDamaged += Game_EventItemDamaged;
            game.EventInventoryChanged += Game_EventInventoryChanged;
            game.EventSkillSetChanged += Game_EventSkillSetChanged;
            game.EventMovement += Game_EventMovement;
        }

        #endregion

        #region Events

        private void Game_EventMovement(object sender, Api.Response.ResponseMovement e)
        {
            Xpos = e.XPos;
            Ypos = e.YPos;
        }

        private void Game_EventSkillSetChanged(object sender, Api.Response.ResponseSkillSetChanged e)
        {
            SkillSet = e.Skillset;
            GameCharacterData = e.GameCharacterData;
            GameCharacter = e.GameCharacter;
        }

        private void Game_EventInventoryChanged(object sender, Api.Response.ResponseInventoryChanged e)
        {
            Inventory.Armors.AddRange(e.ArmorAdded);
            Inventory.Armors.RemoveAll(x => e.ArmorRemoved.Contains(x));

            Inventory.Weapons.AddRange(e.WeaponAdded);
            Inventory.Weapons.RemoveAll(x => e.WeaponRemoved.Contains(x));

            Inventory.Materials.AddRange(e.MaterialAdded);
            Inventory.Materials.RemoveAll(x => e.MaterialRemoved.Contains(x));

            Inventory.Jewels.AddRange(e.JewelAdded);
            Inventory.Jewels.RemoveAll(x => e.JewelRemoved.Contains(x));

            EquipmentSet = e.EquipSet;
            GameCharacter = e.GameCharacter;
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
            Hp = e.Hp;
            Mp = e.Mp;
            LawStatus = e.LawStatus;
            // e.DOTDamage;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_EventCharacterData(object sender, Api.Response.ResponseCharacterData e)
        {
            Xpos = e.XPos;
            Ypos = e.YPos;
            Hp = e.Hp;
        }

        private void Game_EventLogin(object sender, Api.Response.ResponseLogin e)
        {
            ID = e.CharacterID;
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

        public void MoveDown()
        {
            game.SendJavaScript("moveDown();");
        }

        public void MoveLeft()
        {
            game.SendJavaScript("moveLeft();");
        }

        public void MoveRight()
        {
            game.SendJavaScript("moveRight();");
        }

        public void MoveUp()
        {
            game.SendJavaScript("moveUp();");
        }

        public void Stop()
        {
            game.SendJavaScript("requestStop();");
        }

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

        public void ActionBasicRaw(int actionType)
        {
            game.SendJavaScript(string.Format("ReqActBasic({0});", actionType));
        }

        public void Specialskill(int type)
        {
            game.SendJavaScript(string.Format("requestSpecialskill({0});", type));
        }

        public void CastSpell(int spell)
        {
            game.SendJavaScript(string.Format("requestCastSpell({0});", spell));
        }

        public void CastSpellRaw(int chrTgt, int mobTgt, int spell)
        {
            game.SendJavaScript(string.Format("ReqCastSpell({0}, {1}, {2});", chrTgt, mobTgt, spell));
        }


        #endregion
    }
}
