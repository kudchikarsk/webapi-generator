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
            var viewModelUnit = new CodeCompileUnit();
            var className = entity.Name + "ViewModel";
            var viewModelClass = CreateClass(className, "WebApp.ViewModels", viewModelUnit);
            foreach (var item in entity.Properties)
            {
                viewModelClass.AddMember(CreateProperty(item.Name, "System." + item.Type));
            }
            foreach (var navProp in entity.NavigationProperties)
            {
                var relationship = navProp.Relationship.Split('.').Last();
                var end = entity.Associations.Single(a => a.Name == relationship)
                    .Ends.Single(e => e.Role == navProp.ToRole);
                if (end.Multiplicity == "*")
                    viewModelClass.AddMember(CreateProperty(navProp.Name, $"ICollection<Compact{navProp.ToRole}ViewModel>"));
                else
                    viewModelClass.AddMember(CreateProperty(navProp.Name, "Compact"+navProp.ToRole+ "ViewModel"));
            }
            GenerateCSharpCode(className + ".cs", "ViewModels", viewModelUnit);

            //Compact version starts here
            var compactViewModelUnit = new CodeCompileUnit();
            var compactClassName = "Compact" + entity.Name + "ViewModel";
            var compactClass = CreateClass(compactClassName, "WebApp.ViewModels", compactViewModelUnit);
            foreach (var item in entity.Properties)
            {
                compactClass.AddMember(CreateProperty(item.Name, "System." + item.Type));
            }
            GenerateCSharpCode(compactClassName+".cs", "ViewModels", compactViewModelUnit);
        }

        
    }
}
