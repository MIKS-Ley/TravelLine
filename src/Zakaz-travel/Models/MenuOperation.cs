using System;

namespace Zakaz_travel.Models
{
    public class MenuOperation
    {
        public string Text { get; }
        public Action Action { get; }

        public MenuOperation( string text, Action action )
        {
            Text = text;
            Action = action;
        }
    }
}