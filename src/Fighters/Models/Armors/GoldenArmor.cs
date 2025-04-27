using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fighters.Models.Armors;

namespace Fighters.Models.Armors
{
    public class GoldenArmor : IArmor
    {
        public int Armor => 60;
        public ConsoleColor? Color => ConsoleColor.Yellow;
    }
}