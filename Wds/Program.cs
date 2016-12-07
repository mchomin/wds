using System;
using Wds.DAL;
using Wds.PRL;

namespace Wds
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Properties.Settings.Default.JsonDevicesFile;
            IDeviceRepository repo = new JsonDeviceRepository(path);
            var browser = new DevicesBrowser(repo);

            while (true)
            {
                Console.Clear();
                browser.DisplayMenu();
                var choice = Console.ReadKey();
                browser.ActOnChoice(choice);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

}

