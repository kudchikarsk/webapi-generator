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
            var codeCompileUnit = new CodeCompileUnit();
            var className = entity.Name;
            var (@namespace,@class) = CreateClass(className, "WebApp.Models");
            foreach (var item in entity.Properties)
            {
                @class.AddMember(CreateProperty(item.Name, "System." + item.Type));
            }
            foreach (var navProp in entity.NavigationProperties)
            {
                var relationship = navProp.Relationship.Split('.').Last();
                var end = entity.Associations.Single(a => a.Name == relationship)
                    .Ends.Single(e => e.Role == navProp.ToRole);
                if (end.Multiplicity == "*")
                    @class.AddMember(CreateProperty(navProp.Name, $"ICollection<{navProp.ToRole}>"));
                else
                    @class.AddMember(CreateProperty(navProp.Name, navProp.ToRole));
            }
            codeCompileUnit.Namespaces.Add(@namespace);
            GenerateCSharpCode(className + ".cs", "Models", codeCompileUnit);
        }
    }
}
