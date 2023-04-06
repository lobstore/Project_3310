using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    /// <summary>
    /// Перечисление всех опций игры
    /// </summary>
    enum ButtonOption{
        NewGame = 0,
        Exit, 
        Continue,
        Accept,
        Decline
    }
    internal class Button
    {
       public string Text { get; set; }
       public ButtonOption OptionId { get; set; }
       public Button(string Text, ButtonOption OptionId) {
            this.Text = Text;
            this.OptionId = OptionId;
        }
    }
}
