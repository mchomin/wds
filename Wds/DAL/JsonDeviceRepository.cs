using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Wds.DAL
{
    public class JsonDeviceRepository : IDeviceRepository
    {
        private List<Device> _records = new List<Device>();

        public string ErrorLog { get; } = string.Empty;

        public JsonDeviceRepository(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile) || !File.Exists(pathToFile))
                throw new ArgumentException("Invalid path", "pathToFile");

            var jsonContent = File.ReadAllText(pathToFile);
            var records = JsonConvert.DeserializeObject<List<Device>>(jsonContent);

            // Find and add duplicate items to error string
            var duplicates = records.GroupBy(x => x.Brand + " " + x.Model)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
            if (duplicates.Count > 0)
                ErrorLog += "Duplicate items in the devices file : " + string.Join(", ", duplicates) + "\n\n";

            // Add any errors coming from the internal items
            ErrorLog += string.Concat(records.Select(x => x.Validate()));

            //Log errors
            if (ErrorLog.Length > 0)
                File.WriteAllText(Properties.Settings.Default.LogFile, ErrorLog);

            // Load valid items into repository
            _records = records.Where(x => !duplicates.Contains(x.Brand + " " + x.Model) && x.Validate() == string.Empty).ToList();
            
        }

        public List<Device> GetAll()
        {
            return _records;
        }

        public List<Device> GetByBrand(string brand)
        {
            return _records.Where(x => x.Brand == brand).ToList();
        }

        public List<Device> GetByModel(string model)
        {
            return _records.Where(x => x.Model == model).ToList();
        }

        public List<Device> GetByName(string name)
        {
            return _records.Where(x => x.Brand + " " + x.Model == name).ToList();
        }
    }
}
