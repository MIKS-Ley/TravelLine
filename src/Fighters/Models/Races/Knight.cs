using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Fighters.Models.Races
{
    public class Knight : IRace
    {
        public int Damage => 20;
        public int Health => 100;
        public int Armor => 20;

        public string Face => $@"
  ___
 /_|_\
 |o|o|
  \-/";

        public string Body => $@"###+###   ^
# ### #  | |
# =*= #  | |
  # #   -----
  * *     |
  = =     -
";
    }
}