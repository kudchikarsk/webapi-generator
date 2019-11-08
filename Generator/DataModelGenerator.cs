using System.CodeDom;
using System.Linq;
using EdmxParser;
using static Generator.CodeDomHelper;

namespace Generator
{
    public class DataModelGenerator : IGenerator
    {
        public void Generate(EntityType entity)
        {
            var dataModelUnit = new CodeCompileUnit();
            var className = entity.Name;
            var modelClass = CreateClass(className, "WebApp.Models", dataModelUnit);
            foreach (var item in entity.Properties)
            {
                modelClass.AddMember(CreateProperty(item.Name, "System." + item.Type));
            }
            foreach (var navProp in entity.NavigationProperties)
            {
                var relationship = navProp.Relationship.Split('.').Last();
                var end = entity.Associations.Single(a => a.Name == relationship)
                    .Ends.Single(e => e.Role == navProp.ToRole);
                if (end.Multiplicity == "*")
                    modelClass.AddMember(CreateProperty(navProp.Name, $"ICollection<{navProp.ToRole}>"));
                else
                    modelClass.AddMember(CreateProperty(navProp.Name, navProp.ToRole));
            }
            GenerateCSharpCode(className + ".cs", "Models", dataModelUnit);
        }
    }
}
