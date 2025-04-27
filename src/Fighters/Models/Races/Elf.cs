using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Fighters.Models.Races
{
    public class Elf : IRace
    {
        public int Damage => 5;
        public int Health => 105;
        public int Armor => 20;

        public string Face => $@"
   __
 //  \
|^ o|o^
||\ -/";

        public string Body => $@" /|::|\ |\
 ||::|| |-|>
  |**|  |/
  |  |
  *  *
";
    }
}