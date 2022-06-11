using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes.GameClasses
{
    public enum Stats
    {
        Armor = 1,
        Health,
        Damage
    }

    public enum EquipmentTypes
    {
        Sword = 1,
        Armor,
        Shield,
        Bow,
        Hat,
        Dagger,
        Crossbow,
        Staff
    }

    internal class Equipment
    {
        private string _name;
        private EquipmentTypes _equipmentType;
        private Stats _targetStat;
        private float _valueOfStat;
        private bool _isEquip = false;

        public Equipment(string name, EquipmentTypes equipmentType, Stats targetStat, float valueOfStat)
        {
            _name = name;
            _equipmentType = equipmentType;
            _targetStat = targetStat;
            _valueOfStat = valueOfStat;
        }

        public string GetName()
        {
            return _name;
        }

        public EquipmentTypes GetItemType()
        {
            return _equipmentType;
        }

        public Stats GetTargetStat()
        {
            return _targetStat;
        }

        public float GetValueOfStat()
        {
            return _valueOfStat;
        }

        public bool GetIsEquip()
        {
            return _isEquip;
        }
    }
}
