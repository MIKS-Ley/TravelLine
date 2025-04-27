
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Fighters.Models.Races
{
    public class Goblin : IRace
    {
        public int Damage => 1;
        public int Health => 80;
        public int Armor => 5;

        public string Face => $@"
  ___
<|°|°|>
  \-/";

        public string Body => $@"***$***
| *** |/
  | |
  = =
";
    }
}