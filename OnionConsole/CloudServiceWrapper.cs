using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace OnionConsole
{
    public class CloudServiceWrapper
    {
        private RoleEntryPoint _workerRole;
        private readonly string _directoryPath;
        public CloudServiceWrapper(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public IEnumerable<Type> FindRoleEntryTypes()
        {
            DirectoryInfo binDir = new DirectoryInfo(_directoryPath);
            IEnumerable<FileInfo> assemblyFiles = binDir.EnumerateFiles("*.dll", SearchOption.AllDirectories);
            return GetMatchingTypesInAssembly(assemblyFiles, type => typeof(RoleEntryPoint).IsAssignableFrom(type));
        }

        /// <summary>
        /// Get the types within the assembly that match the predicate.
        /// <para>for example, to get all types within a namespace</para>
        /// <para>    typeof(SomeClassInAssemblyYouWant).Assembly.GetMatchingTypesInAssembly(item => "MyNamespace".Equals(item.Namespace))</para>
        /// </summary>
        /// <param name="assemblyFiles">The assembly to search</param>
        /// <param name="predicate">The predicate query to match against</param>
        /// <returns>The collection of types within the assembly that match the predicate</returns>
        public static ICollection<Type> GetMatchingTypesInAssembly(IEnumerable<FileInfo> assemblyFiles, Predicate<Type> predicate)
        {
            ICollection<Type> types = new List<Type>();
            foreach (var file in assemblyFiles)
            {
                var assembly = Assembly.LoadFile(file.FullName);
                try
                {
                    types = assembly.GetTypes().Where(i => i != null && predicate(i) && i.Assembly == assembly).ToList();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (Type theType in ex.Types)
                    {
                        try
                        {
                            if (theType != null && predicate(theType) && theType.Assembly == assembly)
                                types.Add(theType);
                        }
                        catch (Exception exp)
                        {
                            // Type not in this assembly 
                            Debug.WriteLine($"Unable to Load Type in Assembly {exp.Message}");
                        }
                    }
                }

                if (types.Any())
                    return types;
            }
            return types;
        }


        private void RunRole(Type roleType)
        {
            try
            {
                // does this also apply to web roles??
                if (!roleType.Name.Contains("Worker"))
            {
                Console.WriteLine("Unable to run a non worker role");
                return;
            }
            
            _workerRole = (RoleEntryPoint)Activator.CreateInstance(roleType);

            if (!_workerRole.OnStart())
            {
                Console.WriteLine("Role failed to start for '{0}'", roleType);
                return;
            }

                Console.WriteLine("Role failed to start for '{0}'", roleType);
                _workerRole.Run();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Run Role" + ex.GetBaseException());
                _workerRole.OnStop();
            }
        }


        public void StartCloudService()
        {
            var roleToRun = FindRoleEntryTypes()?.FirstOrDefault();
            if (roleToRun != null)
            {
                Console.WriteLine($"Run Role Called for Role {roleToRun?.FullName}");
                RunRole(FindRoleEntryTypes().FirstOrDefault());

            }
        }
    }
}
