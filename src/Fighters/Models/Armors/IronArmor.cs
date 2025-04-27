using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighters.Models.Armors
{
    public class IronArmor : IArmor
    {
        public int Armor => 40;
        public ConsoleColor? Color => ConsoleColor.Gray;
    }
}