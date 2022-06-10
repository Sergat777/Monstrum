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

        public static byte DificultLevel { get; set; } = 1;
        public static byte Chapter { get; set; } = 1;
        public static string HeroName => "dificultLevel" + DificultLevel;
        public static float HeroHealth { get; set; } = 27 / DificultLevel;
        public static float HeroDamage { get; set; } = 9 / DificultLevel;
        public static float HeroArmor{ get; set; } = 0;
        public static float KillCounter { get; set; } = 0;
        public static float EnemiesCounter { get; set; } = 10;
        public static GameClasses.MonsterView Hero { get; set; }
        public static GameClasses.MonsterView Enemy { get; set; }
        public static byte MonsterBottomIndex { get; set; } = 1;
        public static byte MonsterTopIndex { get; set; } = 7;

        public static GameClasses.Monster GenerateMonster()
        {
            string monsterName = "monster" + rndm.Next(MonsterBottomIndex, MonsterTopIndex + 1);
            GameClasses.Monster monster =
                new GameClasses.Monster(monsterName,
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex * DificultLevel) * 2.5F, 1),
                            (float)Math.Round(rndm.Next(MonsterBottomIndex, MonsterTopIndex * DificultLevel) * 1.5F, 1));
            return monster;
        }
    }
}
