using LightenDark.Api.Models;
using LightenDark.Api.Response;
using LightenDark.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        void AutoAttack(int? mobID, int? characterID);
        void CastSpell(int spell);
        void Hunting(int mobID);
        void ItemUse(int itemID, string itemCode);
        void Meditation();
        void Remedy(int characterID);
        void SendMessage(string text, string type);
        void Specialskill(int type);
        void Stop();
        Task<ResponseCastSpell> CastSpellAsync(int chrTgt, int mobTgt, int spell, int timeout = Timeout.Infinite);

        void ActionBasic(int type);
        void ActionBasic(GartheringType type);
        void ActionBasic(ActionBasicType type);
        void Garthering(int type);
        void MoveDown();

        Task<ResponseMovement> MoveDownAsync(int timeout = -1);
        Task<ResponseMovement> MoveLeftAsync(int timeout = -1);
        Task<ResponseMovement> MoveRightAsync(int timeout = -1);
        Task<ResponseMovement> MoveUpAsync(int timeout = -1);
    }
}
