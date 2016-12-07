using System.Collections.Generic;

namespace Wds.DAL
{
    public interface IDeviceRepository
    {
        List<Device> GetAll();
        List<Device> GetByName(string name);
        List<Device> GetByBrand(string brand);
        List<Device> GetByModel(string model);
        string ErrorLog { get; }
    }
}
