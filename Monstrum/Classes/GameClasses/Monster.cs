using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes.GameClasses
{
    internal class Monster
    {
        private Random rndm = new Random();

        private string _name;
        private float _maxHealth;
        private float _health;
        private float _damage;
        private float _armor;

        public Monster(string name, float maxHealth, float damage, float armor = 0)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _damage = damage;
            _armor = armor;
        }

        // getters
        public float GetMaxHealth()
        {
            return _maxHealth;
        }

        public float GetHealth()
        {
            return _health;
        }

        public float GetDamage()
        {
            return _damage;
        }

        public float GetArmor()
        {
            return _armor;
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

        public void ApplyDamage(float incommingDamage)
        {
            if (incommingDamage > _armor)
                if (_health - incommingDamage <= 0)
                    _health = 0;
                else
                    _health -= incommingDamage - _armor;

            CheckHealth();
        }

        public string GetName()
        {
            return _name;
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
