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
    internal class EquipmentView : Image
    {
        private Equipment _equipment;


        public EquipmentView(Equipment equipment)
        {
            _equipment = equipment;

            Source = new BitmapImage(new Uri(MediaHelper.AmunitionsPath + _equipment.GetName() + ".png"));
            Stretch = Stretch.Fill;
        }

        public EquipmentTypes GetEquipmentType()
        {
            return _equipment.GetItemType();
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
