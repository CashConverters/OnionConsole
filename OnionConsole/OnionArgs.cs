using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;

namespace OnionConsole
{
    public class OnionArgs
    {
        public string EntryAssembly { get; private set; }

        private OptionSet options = new OptionSet
        {
           
        };

        public void ParseArgs(IEnumerable<string> args)
        {

                     
        }
    }
}
