using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighters.Models.Armors
{
    public class DiamondArmor : IArmor
    {
        public int Armor => 90;
        public ConsoleColor? Color => ConsoleColor.Cyan;
    }
}
