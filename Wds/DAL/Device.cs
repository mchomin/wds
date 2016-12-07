using System.Collections.Generic;
using System.Linq;

namespace Wds.DAL
{
    public class Device
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty;
        public List<Attribute> Attributes { get; set; } = new List<Attribute>();

        public string Validate()
        {
            var errors = string.Empty;
            if (Brand.Length == 0 || Brand.Length > 50)
                errors += "Invalid brand : " + (Brand.Length != 0 ? Brand : "NULL") + "\n\n";
            if (Model.Length == 0 || Model.Length > 50)
                errors += "Invalid model : " + (Model.Length != 0 ? Model : "NULL") + "\n\n";
            if (!Properties.Settings.Default.ValidFormFactors.Split(',').Contains(FormFactor))
                errors += "Invalid form factor : " + (FormFactor.Length != 0 ? FormFactor : "NULL") + "\n\n";
            if (Attributes.Count > 0)
            {
                var errName = (Attributes.Where(x => x.Name.Length == 0 || x.Name.Length > 20).Select(x=> x.Name));
                if (errName.Count() > 0)
                    errors += "Invalid attribute names : " + string.Join(", ", errName.Select(x=> x.Length != 0 ? x : "NULL")) + " in " + Brand + " " + Model + "\n\n";

                var errValue = (Attributes.Where(x => x.Value.Length == 0 || x.Value.Length > 100).Select(x => x.Value));
                if (errValue.Count() > 0)
                    errors += "Invalid attribute values : " + string.Join(", ", errValue.Select(x => x.Length != 0 ? x : "NULL")) + " in " + Brand + " " + Model + "\n\n";
            }

            return errors;
        }
    }

    public class Attribute
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
