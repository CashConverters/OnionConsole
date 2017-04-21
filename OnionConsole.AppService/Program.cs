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
                //todo tidy this up
                var cloudServiceHelper = new CloudServiceHelper(opts.ConfigurationPath);
                var roleEntryTypes = cloudServiceHelper.FindRoleEntryTypes();
                var roleType = roleEntryTypes as IList<Type> ?? roleEntryTypes.ToList();
                if (roleType.Any())
                {
                    var type = roleType.FirstOrDefault();
                    if (type != null)
                    {
                        Console.WriteLine($"Running Role {type.FullName}");
                        cloudServiceHelper.RunRole(type);
                    }
                }
                else
                {
                    Console.WriteLine($"No suitable roles found with {opts.ConfigurationPath}");
                }
            }
            catch (Exception e)
            {
                Trace.TraceError($"Error starting Role {e.Message}");
            }


        }
    }


}
