using System;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionConsole.AppService
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = new OnionArgs();
            
            if (opts.Parser.ParseArguments(args, opts))
            {
                Console.WriteLine(opts.ConfigurationPath);
                // Values are available here
                if (opts.Verbose) Console.WriteLine("Filename: {0}", opts.AssemblyName);
              
            }
            var state = opts.LastParserState;
        
            Console.ReadLine();
        }
    }


}
