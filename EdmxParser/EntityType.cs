using Newtonsoft.Json;
using System.Collections.Generic;

namespace EdmxParser
{
    public class EntityType
    {
        public EntityType(string name, IEnumerable<Property> properties, IEnumerable<NavigationProperty> navigationProperties, IEnumerable<Association> associations)
        {
            Name = name;
            Properties = properties;
            NavigationProperties = navigationProperties;
        }

        public string Name { get; }
        public IEnumerable<Property> Properties { get; }
        public IEnumerable<NavigationProperty> NavigationProperties { get; }
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}