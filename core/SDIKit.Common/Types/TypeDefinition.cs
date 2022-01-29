using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SDIKit.Common.Types
{
    public partial class ApplicationTypeDefinition
    {
        private List<PropertyInfo> properties;

        public ApplicationTypeDefinition()
        {
            properties = new List<PropertyInfo>();
        }

        public IReadOnlyCollection<PropertyInfo> Properties
        {
            get
            {
                properties.ForEach(i => i.Order = i.Order == 0 ? properties.Count : i.Order);
                return properties.OrderBy(i => i.Order).ToList().AsReadOnly();
            }
        }

        public void AddDefinition(PropertyInfo definition)
        {
            properties.Add(definition);
        }

        public PropertyInfo CreateParameter(string name)
        {
            return new PropertyInfo
            {
                Name = name.Substring(0, 1).ToLower(CultureInfo.GetCultureInfo(1033)) + name.Substring(1)
            };
        }
    }
}