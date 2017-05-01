using System;
using System.IO;

namespace OnionConsole.AppService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var cloudServiceHelper = new CloudServiceWrapper(Directory.GetCurrentDirectory());
                cloudServiceHelper.StartCloudService();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error starting Role {e.Message}");
            }


        }
    }


}
