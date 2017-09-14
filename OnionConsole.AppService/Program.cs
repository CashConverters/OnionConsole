using System;
using System.IO;

namespace OnionConsole.AppService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cloudServiceHelper = new CloudServiceWrapper(Directory.GetCurrentDirectory());
            cloudServiceHelper.StartCloudService();
        }
    }
}
