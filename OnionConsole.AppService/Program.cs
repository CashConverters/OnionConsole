using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace OnionConsole.AppService
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = new OnionArgs();
            if (opts.ParseArguments(args, opts))
            {
                if (opts.ConfigurationPath == null)
                    opts.ConfigurationPath = Directory.GetCurrentDirectory();

                if (opts.Verbose)
                {
                    Console.WriteLine("ConfigurationPath: {0}", opts.ConfigurationPath);
                    Console.WriteLine("AssemblyName: {0}", opts.AssemblyName);
                }
            }

            try
            {
                var cloudServiceHelper = new CloudServiceHelper(opts.ConfigurationPath);
                cloudServiceHelper.StartCloudService();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error starting Role {e.Message}");
            }


        }
    }


}
