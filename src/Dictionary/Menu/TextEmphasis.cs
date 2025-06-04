using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perevodchik
{
    public static class TextEmphasis
    {
        public static string Custom( string text, string foreground, string background )
        {
            return $"[{foreground} on {background}]{text}[/]";
        }
    }
}
