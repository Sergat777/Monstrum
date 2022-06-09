using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstrum.Classes
{
    static class GameSetter
    {
        public static byte DificultLevel { get; set; } = 1;
        public static string HeroName => "dificultLevel" + DificultLevel;
        public static byte Chapter { get; set; } = 1;
    }
}
