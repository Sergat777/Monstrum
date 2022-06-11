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

        public static byte DifficultLevel { get; set; } = 1;
        public static byte Chapter { get; set; } = 1;
        public static string HeroName => "difficultLevel" + DifficultLevel;
        public static float HeroHealth => 30 * DifficultLevel / 3 * Chapter;
        public static float HeroDamage =>  2.5F * DifficultLevel * Chapter;
        public static float HeroArmor => Chapter - 1;
        public static float FightsCounter { get; set; } = 0;
        public static float EnemiesCounter { get; set; } = 10;
        public static float KillsCounter { get; set; } = 0;
        public static GameClasses.MonsterView Hero { get; set; }
        public static GameClasses.MonsterView Enemy { get; set; }
        public static byte MonsterBottomIndex { get; set; } = 1;
        public static byte MonsterTopIndex { get; set; } = 7;

        public static GameClasses.Monster GenerateMonster()
        {
            string monsterName = "monster" + rndm.Next(MonsterBottomIndex, MonsterTopIndex + 1);
            GameClasses.Monster monster =
                new GameClasses.Monster(monsterName,
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * (DifficultLevel - 0.25F) * Chapter, 1),
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex) * (DifficultLevel / 4F) * Chapter, 1));
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
                value = 5F / type * Chapter * coolIndex;
            }
            else
            {
                stat = (GameClasses.Stats)rndm.Next(2, 4);
                value = 3.5F * DifficultLevel / Chapter * coolIndex;
            }

            GameClasses.Equipment newEquipment = new GameClasses.Equipment(equipmentName, equipmentType, stat, value);

            return newEquipment;
        }

        public static void IncreaseFightsCounter()
        {
            FightsCounter++;
            if (FightsCounter == EnemiesCounter)
                ;
        }

        public static void SetStandart(int enemyNumber = 10)
        {
            FightsCounter = 0;
            EnemiesCounter = 10;
        }
    }
}
