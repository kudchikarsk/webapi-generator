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
               var (field, prop) =  CreateProperty(item.Name, "System." + item.Type);
                @class.AddMember(field);
                @class.AddMember(prop);
            }
            foreach (var navProp in entity.NavigationProperties)
            {
                var relationship = navProp.Relationship.Split('.').Last();
                var end = entity.Associations.Single(a => a.Name == relationship)
                    .Ends.Single(e => e.Role == navProp.ToRole);
                if (end.Multiplicity == "*")
                {
                    var (field, prop) = CreateProperty(navProp.Name, $"ICollection<{navProp.ToRole}>");
                    @class.AddMember(field);
                    @class.AddMember(prop);
                }
                else
                {
                    var (field, prop) = CreateProperty(navProp.Name, navProp.ToRole);
                    @class.AddMember(field);
                    @class.AddMember(prop);
                }
            }
            codeCompileUnit.Namespaces.Add(@namespace);
            codeCompileUnit.ReferencedAssemblies.Add("System.Collections.Generic");
            GenerateCSharpCode(className + ".cs", "Models", codeCompileUnit);
        }
    }
}
