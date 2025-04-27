using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Fighters.Models.Races
{
    public class Mag : IRace
    {
        public int Damage => 30;
        public int Health => 170;
        public int Armor => 25;

        public string Face => $@"
  /\
_/__\_
 |o|o|
 \\-//";

        public string Body => $@" / ()\ (°)
| | ||  |
|====|  |
||||||  |
||||||  |
 =  =   |
";
    }
}