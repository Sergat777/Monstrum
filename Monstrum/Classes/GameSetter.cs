using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes
{
    static class GameSetter
    {
        private static Random rndm = new Random();

        public static byte DifficultLevel { get; set; } = 3;
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
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * BloodIndex * (DifficultLevel / 3F) * Chapter, 1),
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * BloodIndex * (DifficultLevel / 5F) * Chapter, 1));
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
                value = 5F / type / BloodIndex * Chapter * coolIndex;
            }
            else
            {
                stat = (GameClasses.Stats)rndm.Next(2, 4);
                value = 3.5F * DifficultLevel / Chapter / BloodIndex * coolIndex;
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
                Boss = new GameClasses.MonsterView(new GameClasses.Boss("CAKE", 7 * DifficultLevel * Chapter * BloodIndex,
                    (9 + DifficultLevel) / 3 * Chapter * BloodIndex, rndm.Next(1, 4 * DifficultLevel) * Chapter));
                new Windows.BossWindow(Boss.GetMonster().GetName() + "!").ShowDialog();
            }
            else if (FightsCounter == EnemiesCounter * 0.5)
                new Windows.RewardWindow().ShowDialog();

        }

        public static void SetStandart()
        {
            BloodIndex = StartBloodIndex;
            FightsCounter = 0;
            EnemiesCounter = MonsterTopIndex;
            Boss = null;

            if (Chapter == 1)
            {
                Weapon = null;
                Hat = null;
                Armor = null;
                Shield = null;
                InventoryList = new List<GameClasses.EquipmentView>();
            }
        }

        public static void SetNextChapter()
        {
            StartBloodIndex = BloodIndex;
            Chapter++;
        }
    }
}
