using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Fighters.Models.Races
{
    public class Viking : IRace
    {
        public int Damage => 40;
        public int Health => 120;
        public int Armor => 15;

        public string Face => $@"
    _
  ?/ \?
  |o|o|
  \(-)/";

        public string Body => $@" *******
 #\###/#
 # === # ----
   # #     \/
   # #
   = =
";
    }
}