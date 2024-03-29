﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes
{
    static class GameSetter
    {
        private static Random rndm = new Random();

        public static byte DifficultLevel { get; set; } = 2;
        public static byte Chapter { get; set; } = 1;
        public static byte MonsterBottomIndex => (byte)((Chapter * 10) - 9);
        public static byte MonsterTopIndex => (byte)(Chapter * 10);


        public static GameClasses.MonsterView Hero { get; set; }
        public static GameClasses.MonsterView Enemy { get; set; }


        public static string HeroName => "difficultLevel" + DifficultLevel;
        public static float HeroMaxHealth => 30 * DifficultLevel / 3 * Chapter + AdditionalHealth;
        public static float HeroCurrentHealth => Hero.GetMonster().GetHealth();
        public static float HeroDamage =>  2.5F * DifficultLevel * Chapter + AdditionalDamage;
        public static float HeroArmor => Chapter - 1 + AdditionalArmor;
        public static float AdditionalHealth { get; set; } = 0;
        public static float AdditionalDamage { get; set; } = 0;
        public static float AdditionalArmor { get; set; } = 0;
        public static List<GameClasses.EquipmentView> InventoryList { get; set; } = new List<GameClasses.EquipmentView>();
        public static GameClasses.EquipmentView Hat { get; set; }
        public static GameClasses.EquipmentView Armor { get; set; }
        public static GameClasses.EquipmentView Weapon { get; set; }
        public static GameClasses.EquipmentView Shield { get; set; }
        public static List<GameClasses.EquipmentView> StartInventoryList { get; set; } = new List<GameClasses.EquipmentView>();
        public static GameClasses.EquipmentView StartHat { get; set; } = null;
        public static GameClasses.EquipmentView StartArmor { get; set; } = null;
        public static GameClasses.EquipmentView StartWeapon { get; set; } = null;
        public static GameClasses.EquipmentView StartShield { get; set; } = null;


        public static float TotalDamage { get; set; } = 0;
        public static float FightsCounter { get; set; } = 0;
        public static float EnemiesCounter { get; set; } = 10;
        public static float KillsCounter { get; set; } = 0;
        public static float BloodIndex { get; set; } = 1F;
        public static float StartBloodIndex { get; set; } = 1F;
        public static GameClasses.MonsterView Boss { get; set; } = null;


        public static GameClasses.Monster GenerateMonster()
        {
            string monsterName = "monster" + rndm.Next(MonsterBottomIndex, MonsterTopIndex + 1);
            GameClasses.Monster monster =
                new GameClasses.Monster(monsterName,
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * BloodIndex * Chapter * (DifficultLevel / 3F), 1),
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * BloodIndex * (DifficultLevel / 3.25F), 1),
                            rndm.Next(Chapter - 1, MonsterBottomIndex / 2));
            return monster;
        }

        public static GameClasses.Equipment GenerateEquipment()
        {
            int type;
            if (Chapter < 4)
                type = rndm.Next(1, 3 + Chapter);
            else
                type = rndm.Next(1, 8); // unblock after third chapter

            GameClasses.EquipmentTypes equipmentType = (GameClasses.EquipmentTypes)type;
            string equipmentName = Enum.GetName(typeof (GameClasses.EquipmentTypes), type).ToLower();

            int coolIndex = 1;

            if (type == 2)    // armor 7
                if (Chapter < 4)
                    coolIndex = rndm.Next(1, 3);
                else
                    coolIndex = rndm.Next(3, 8);

            if (type == 1 || type == 5) // sword & hat 5
                if (Chapter < 3)
                    coolIndex = rndm.Next(1, 2);
                else
                    coolIndex = rndm.Next(2, 6);

            if (type == 3) // shield 3
                coolIndex = rndm.Next(1, 4);

            if (type == 4 || (int)equipmentType >= 6) // bow & crossbow & dagger 4
                coolIndex = rndm.Next(1, Chapter - 1);

            equipmentName += coolIndex;

            GameClasses.Stats stat;
            float value;

            if (type == 2 || type == 3 || type == 5)
            {
                stat = (GameClasses.Stats)rndm.Next(1, 3);
                value = 5F / type * BloodIndex * Chapter * coolIndex;
            }
            else
            {
                stat = (GameClasses.Stats)rndm.Next(2, 4);
                value = 3.5F * DifficultLevel / Chapter * BloodIndex * coolIndex;
            }

            GameClasses.Equipment newEquipment = new GameClasses.Equipment(equipmentName, equipmentType, stat, value);

            return newEquipment;
        }

        public static void UpdateStats()
        {
            int statType;

            float armor = 0;
            float health = 0;
            float damage = 0;

            // Weapon
            if (Weapon != null)
            {
                statType = (int)Weapon.GetEquipmentStat();

                if (statType == 2)
                    health += Weapon.GetEquipmentStatValue();
                else
                    damage += Weapon.GetEquipmentStatValue();
            }

            // Hat
            if (Hat != null)
            {
                statType = (int)Hat.GetEquipmentStat();

                if (statType == 1)
                    armor += Hat.GetEquipmentStatValue();
                else
                    health += Hat.GetEquipmentStatValue();
            }

            // Armor
            if (Armor != null)
            {
                statType = (int)Armor.GetEquipmentStat();

                if (statType == 1)
                    armor += Armor.GetEquipmentStatValue();
                else
                    health += Armor.GetEquipmentStatValue();
            }

            // Shield
            if (Shield != null)
            {
                statType = (int)Shield.GetEquipmentStat();

                if (statType == 1)
                    armor += Shield.GetEquipmentStatValue();
                else
                    health += Shield.GetEquipmentStatValue();
            }

            // Update monster
            GameClasses.Monster hero = Hero.GetMonster();

            hero.SetHealth(HeroCurrentHealth - AdditionalHealth);

            AdditionalArmor = armor;
            AdditionalHealth = health;
            AdditionalDamage = damage;

            hero.SetMaxHealth(HeroMaxHealth);
            hero.SetHealth(HeroCurrentHealth + AdditionalHealth);
            hero.SetDamage(HeroDamage);
            hero.SetArmor(HeroArmor);

            Hero.UpdateHealthPanel();
        }

        public static void IncreaseFightsCounter()
        {
            FightsCounter++;
            if (Boss != null)
            {
                if (Boss.IsEscaped())
                    MediaHelper.SetGameMusic("homeMusic");
                else
                    MediaHelper.SetGameMusic("bossMusic");

                new Windows.PreWinWindow(Boss.GetMonster().GetName()).ShowDialog();

                new Windows.RewardWindow().ShowDialog();
                new Windows.RewardWindow().ShowDialog();
                new Windows.WinWindow().ShowDialog();
            }
            else if (FightsCounter >= EnemiesCounter)
            {
                for (int i = 0; i < DifficultLevel; i++)
                    new Windows.RewardWindow().ShowDialog();

                MediaHelper.SetGameMusic("bossMusic");
                Boss = new GameClasses.MonsterView(new GameClasses.Boss(Chapter, 10 * DifficultLevel * Chapter * BloodIndex,
                    (12 + DifficultLevel) / 3 * Chapter * BloodIndex, rndm.Next(1, 3 * DifficultLevel) * Chapter));
                new Windows.BossWindow(Boss.GetMonster().GetName() + "!").ShowDialog();
            }
            else if (FightsCounter % 7 == 0 && FightsCounter != 0)
                for (int i = 0; i <= (DifficultLevel + 1) / 2; i++)
                    new Windows.RewardWindow().ShowDialog();

        }

        public static void SetStandart()
        {
            TotalDamage = 0;
            BloodIndex = StartBloodIndex;
            FightsCounter = 0;
            EnemiesCounter = MonsterTopIndex;
            Boss = null;
            KillsCounter = 0;

            Weapon = StartWeapon;
            Hat = StartHat;
            Armor = StartArmor;
            Shield = StartShield;

            InventoryList.Clear();

            foreach (var i in StartInventoryList)
                InventoryList.Add(i);
        }

        public static void SetNextChapter()
        {
            StartBloodIndex = BloodIndex;

            StartInventoryList.Clear();

            foreach (var i in InventoryList)
                StartInventoryList.Add(i);

            StartHat = Hat;
            StartArmor = Armor;
            StartShield = Shield;
            StartWeapon = Weapon;
            Chapter++;
        }
    }
}
