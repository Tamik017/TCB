using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Model
{
    public class Command
    {
        public int Number { get; set; }
        public string Cmd { get; set; }
        public string CommandText { get; set; }

        public void UpdateIndex(int index)
        {
            Number = index;
        }
    }
}
