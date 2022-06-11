using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes.GameClasses
{
    internal class Monster
    {
       // private Random rndm = new Random();

        private string _name;
        private float _maxHealth;
        private float _health;
        private float _damage;
        private float _armor;
        private bool _isBlock = false;
        private bool _isEscaped = false;

        public Monster(string name, float maxHealth, float damage, float armor = 0)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _damage = damage;
            _armor = armor;
        }

        // getters
        public string GetName()
        {
            return _name;
        }

        public float GetMaxHealth()
        {
            return _maxHealth;
        }

        public float GetHealth()
        {
            return (float)Math.Round(_health, 1);
        }

        public float GetDamage()
        {
            return _damage;
        }

        public float GetArmor()
        {
            return _armor;
        }

        public bool GetIsEscaped()
        {
            return _isEscaped;
        }


        // setters
        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetArmor(float armor)
        {
            _armor = armor;
        }

        public bool TryRun(float potentionalDamage)
        {
            if (potentionalDamage >= _health)
            {
                _name = "byebye";
                _isEscaped = true;
            }

            return potentionalDamage >= _health;
        }

        public void OnBlock()
        {
            _isBlock = true;

            if (_armor != 0)
                _armor *= 2;
            else
                _armor = 1;
        }

        public void OffBlock()
        {
            if (_isBlock)
            {
                _isBlock = false;
                if (_armor != 1)
                    _armor /= 2;
                else
                    _armor = 0;
            }
        }

        public void ApplyDamage(float incommingDamage)
        {
            if (incommingDamage > _armor)
                if (_health - incommingDamage <= 0)
                    _health = 0;
                else
                    _health -= incommingDamage - _armor;

            OffBlock();

            CheckHealth();
        }

        public void CheckHealth()
        {
            if (_health > _maxHealth)
                _health = _maxHealth;
            if (_health <= 0)
                Die();
        }

        public void Die()
        {
            _name = "skull";
        }
    }
}
