using LightenDark.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Interfaces
{
    public interface IPlayer
    {
        string DisplayName { get; set; }
        EquipSetModel EquipmentSet { get; }
        GameCharacterModel GameCharacter { get; set; }
        GameCharacterDataModel GameCharacterData { get; set; }
        int Hp { get; }
        int ID { get; set; }
        InventoryModel Inventory { get; }
        byte LawStatus { get; }
        int Mp { get; }
        SkillSetModel SkillSet { get; }
        short Xpos { get; }
        short Ypos { get; }

        void ActionBasicRaw(int actionType);
        void AutoAttack(int? mobID, int? characterID);
        void CastSpell(int spell);
        void CastSpellRaw(int chrTgt, int mobTgt, int spell);
        void Hunting(int mobID);
        void ItemUse(int itemID, string itemCode);
        void Meditation();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void Remedy(int characterID);
        void SendMessage(string text, string type);
        void Specialskill(int type);
        void Stop();
    }
}
