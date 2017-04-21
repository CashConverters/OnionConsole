using CommandLine;
using CommandLine.Text;

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

        [Option('c', "configurationpath", Required = false, MutuallyExclusiveSet = "set",
            HelpText = "The {PATH} to the configuration file. Either the directory containing application config or the path to a specific alternate config file." +
                       " Defaults to current directory")]
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
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
          public Parser Parser { get; }

        public bool ParseArguments(string[] args, OnionArgs opts)
        {
            return Parser.ParseArguments(args, opts);
        }
    }

  }
