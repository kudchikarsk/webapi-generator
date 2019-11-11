using System.CodeDom;
using System.IO;
using System.Linq;
using EdmxParser;
using Microsoft.Build.Evaluation;
using static Generator.CodeDomHelper;

namespace Generator
{
    public class DataModelGenerator : IGenerator
    {
        private readonly Project proj;        

        public DataModelGenerator(Project proj)
        {
            this.proj = proj;
        }

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

            var fileName = className + ".cs";
            var folder = "Models";
            GenerateCSharpCode(fileName, folder, codeCompileUnit);
            proj.RemoveItems(proj.GetItemsByEvaluatedInclude(Path.Combine(folder, fileName)));
            proj.AddItem("Compile", Path.Combine(folder, fileName));
        }
    }
}
