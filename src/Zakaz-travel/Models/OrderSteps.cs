using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakaz_travel.Models
{
    public class OrderSteps
    {
        public record OrderStep(
    string Prompt,
    Action<string> ValueSetter,
    Func<string, bool> Validator,
    string ErrorMessage
    );
    }
}
