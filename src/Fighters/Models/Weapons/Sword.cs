using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Меч
namespace Fighters.Models.Weapons
{
    internal class Sword : IWeapon
    {
        public int Damage => 30;
    }
}