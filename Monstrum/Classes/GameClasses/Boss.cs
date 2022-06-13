using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes.GameClasses
{
    internal class Boss : Monster
    {
        private byte _chapter;

        public Boss(int chapter, float maxHealth, float damage, float armor = 2) : base(null ,maxHealth, damage, armor)
        {
            _chapter = (byte)chapter;
            if (chapter == 1)
                _name = "CAKE";
            else
                _name = "LEAD";
            _maxHealth = maxHealth;
            _damage = damage;
            _armor = armor;
        }


        public override string GenerateSpeach()
        {
            string speach = _name;
            if (_health <= 0)
                speach += "bloodSpeach";
            else if (_isEscaped)
                speach += "escapeSpeach";
            else if (_isAttacked)
                speach += "simpleSpeach";
            else
                speach += "angrySpeach";

            speach += _rndm.Next(1, 3 + _chapter);
            speach = MediaHelper.GetSpeach(speach);

            return speach;
        }

        public override void Die()
        {
            _name += "-";
        }

        public override bool TryRun(float potentionalDamage)
        {
            if (potentionalDamage - _armor >= _health)
            {
                _name += "+";
                _isEscaped = true;
            }
            else
                _isAttacked = false;

            return potentionalDamage >= _health;
        }
    }
}
