using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace OnionConsole
{
    public class CloudServiceHelper
    {
        private RoleEntryPoint _workerRole;
        private readonly string _directoryPath;
        public CloudServiceHelper(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public IEnumerable<Type> FindRoleEntryTypes()
        {
            DirectoryInfo binDir = new DirectoryInfo(_directoryPath);
            var types = binDir.EnumerateFiles("*.dll", SearchOption.AllDirectories)
                .Select(f => Assembly.LoadFile(f.FullName))
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes().Where(t => typeof(RoleEntryPoint).IsAssignableFrom(t));
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        return e.Types.Where(t => typeof(RoleEntryPoint).IsAssignableFrom(t));
                    }
                });

          return types;
        }

        
       public void RunRole(Type roleType)
        {
            // does this also apply to web roles??
            if (!roleType.Name.Contains("Worker"))
            {
                Trace.TraceError("Unable to run  a non worker role");
                return;
            }
            
            _workerRole = (RoleEntryPoint)Activator.CreateInstance(roleType);

            if (!_workerRole.OnStart())
            {
                Trace.TraceError("Role failed to start for '{0}'", roleType);
                return;
            }
            try
            {
                _workerRole.Run();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Unable to Run Role" + ex.Message);
            }
            finally
            {
                _workerRole.OnStop();
            }


        }




    }
}
