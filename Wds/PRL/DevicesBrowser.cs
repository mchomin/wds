using System;
using Wds.DAL;

namespace Wds.PRL
{
    public class DevicesBrowser
    {
        private IDeviceRepository _repo;

        public DevicesBrowser(IDeviceRepository repo)
        {
            _repo = repo;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Please choose:");
            Console.WriteLine("1. List all devices");
            Console.WriteLine("2. Get by full name");
            Console.WriteLine("3. Get by model");
            Console.WriteLine("4. Get by brand");
            Console.WriteLine("9. Exit");
        }

        public void ActOnChoice(ConsoleKeyInfo choice)
        {
            if (choice.KeyChar == '1')
            {
                Console.Clear();
                foreach (var d in _repo.GetAll())
                {
                    DisplayDevice(d);
                }
            }
            else if (choice.KeyChar == '2')
            {
                Console.Clear();
                Console.WriteLine("Please type the device name: ");
                var name = Console.ReadLine();
                Console.Clear();
                foreach (var d in _repo.GetByName(name))
                {
                    DisplayDevice(d);
                }
            }
            else if (choice.KeyChar == '3')
            {
                Console.Clear();
                Console.WriteLine("Please type the device model: ");
                var model = Console.ReadLine();
                Console.Clear();
                foreach (var d in _repo.GetByModel(model))
                {
                    DisplayDevice(d);
                }
            }
            else if (choice.KeyChar == '4')
            {
                Console.Clear();
                Console.WriteLine("Please type the device brand: ");
                var brand = Console.ReadLine();
                Console.Clear();
                foreach (var d in _repo.GetByBrand(brand))
                {
                    DisplayDevice(d);
                }
            }
            else if (choice.KeyChar == '9')
            {
                Environment.Exit(0);
            }
        }

        private void DisplayDevice(Device device)
        {
            Console.WriteLine("Brand: " + device.Brand);
            Console.WriteLine("Model: " + device.Model);
            Console.WriteLine("Form Factor: " + device.FormFactor);
            if (device.Attributes.Count > 0) Console.WriteLine("Attributes:");
            foreach (DAL.Attribute a in device.Attributes)
            {
                Console.WriteLine("\t" + a.Name + " : " + a.Value);
            }
            Console.WriteLine();

        }
    }
}
