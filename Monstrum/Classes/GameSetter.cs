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
