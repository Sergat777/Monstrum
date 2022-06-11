using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Monstrum.Classes.GameClasses
{
    internal class EquipmentView
    {
        private Equipment _equipment;
        public string ImageSource { get; set; }


        public EquipmentView(Equipment equipment)
        {
            _equipment = equipment;

            ImageSource = MediaHelper.AmunitionsPath + _equipment.GetName() + ".png";
        }

        public Equipment GetEquipment()
        {
            return _equipment;
        }

        public EquipmentTypes GetEquipmentType()
        {
            return _equipment.GetItemType();
        }

        public float GetEquipmentStatValue()
        {
            return (float)Math.Round(_equipment.GetValueOfStat(), 1);
        }

        public Stats GetEquipmentStat()
        {
            return _equipment.GetTargetStat();
        }

        public string GetEquipmentName()
        {
            return _equipment.GetName();
        }

        public string GetEquipmentDescription()
        {
            return MediaHelper.GetItemDescription(_equipment.GetName()); ;
        }

        public bool IsEquipped()
        {
            return _equipment.GetIsEquip();
        }
    }
}
