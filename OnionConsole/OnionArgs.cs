using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using NDesk.Options;

namespace OnionConsole
{
    public class OnionArgs
    {
        public OnionArgs()
        {
            Parser = new Parser(Configuration);
        }

        private void Configuration(ParserSettings parserSettings)
        {
            parserSettings.CaseSensitive = false;
            parserSettings.MutuallyExclusive = true;
        }

        [Option('a', "assemblyname", Required = false, MutuallyExclusiveSet = "set",
            HelpText = "The path to the primary {ASSEMBLY} for the worker role.")]
        public string AssemblyName { get; set; }

        [Option('c', "configurationpath", DefaultValue = ".\\", Required = false, MutuallyExclusiveSet = "set",
            HelpText = "The {PATH} to the configuration file. Either the directory containing ServiceConfiguration.Local.cscfg or the path to a specific alternate .cscfg file.")]
        public string ConfigurationPath { get; set; }


        [Option('v', "verbose", DefaultValue = true,
            HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

          public Parser Parser { get; }
    }

  }
