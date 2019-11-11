using System.CodeDom;
using System.Linq;
using EdmxParser;
using static Generator.CodeDomHelper;

namespace Generator
{
    public class ViewModelGenerator : IGenerator
    {
        public void Generate(EntityType entity)
        {
            GenerateViewModel(entity);
            GenerateCompactViewModel(entity);
        }

        private static void GenerateViewModel(EntityType entity)
        {
            var codeCompileUnit = new CodeCompileUnit();
            var className = entity.Name + "ViewModel";
            var (@namespace,@class) = CreateClass(className, "WebApp.ViewModels");
            foreach (var item in entity.Properties)
            {
                var (field, prop) = CreateProperty(item.Name, "System." + item.Type);
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
                    var (field, prop) = CreateProperty(navProp.Name, $"ICollection<Compact{navProp.ToRole}ViewModel>");
                    @class.AddMember(field);
                    @class.AddMember(prop);
                }
                else
                {
                    var (field, prop) = CreateProperty(navProp.Name, "Compact" + navProp.ToRole + "ViewModel");
                    @class.AddMember(field);
                    @class.AddMember(prop);
                }
            }
            codeCompileUnit.Namespaces.Add(@namespace);
            GenerateCSharpCode(className + ".cs", "ViewModels", codeCompileUnit);
        }

        private static void GenerateCompactViewModel(EntityType entity)
        {
            var codeCompileUnit = new CodeCompileUnit();
            var className = "Compact" + entity.Name + "ViewModel";
            var (ns, @class) = CreateClass(className, "WebApp.ViewModels");
            foreach (var item in entity.Properties)
            {
                var (field, prop) = CreateProperty(item.Name, "System." + item.Type);
                @class.AddMember(field);
                @class.AddMember(prop);
            }
            codeCompileUnit.Namespaces.Add(ns);
            GenerateCSharpCode(className + ".cs", "ViewModels", codeCompileUnit);
        }
    }
}
