using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes.GameClasses
{
    internal class Boss : Monster
    {
        public Boss(string name, float maxHealth, float damage, float armor = 2) : base(name, maxHealth, damage, armor)
        {
            _name = name;
            _maxHealth = maxHealth;
            _damage = damage;
            _armor = armor;
        }
    }
}
