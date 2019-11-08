using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EdmxParser;

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
                viewModelClass.AddProperty(CreateProperty(item.Name, "System." + item.Type));
            }
            foreach (var navProp in entity.NavigationProperties)
            {
                var relationship = navProp.Relationship.Split('.').Last();
                var end = entity.Associations.Single(a => a.Name == relationship)
                    .Ends.Single(e => e.Role == navProp.ToRole);
                if (end.Multiplicity == "*")
                    viewModelClass.AddProperty(CreateProperty(navProp.Name, $"ICollection<Compact{navProp.ToRole}ViewModel>"));
                else
                    viewModelClass.AddProperty(CreateProperty(navProp.Name, "Compact"+navProp.ToRole+ "ViewModel"));
            }
            GenerateCSharpCode(className + ".cs", "ViewModels", viewModelUnit);

            //Compact version starts here
            var compactViewModelUnit = new CodeCompileUnit();
            var compactClassName = "Compact" + entity.Name + "ViewModel";
            var compactClass = CreateClass(compactClassName, "WebApp.ViewModels", compactViewModelUnit);
            foreach (var item in entity.Properties)
            {
                compactClass.AddProperty(CreateProperty(item.Name, "System." + item.Type));
            }
            GenerateCSharpCode(compactClassName+".cs", "ViewModels", compactViewModelUnit);
        }

        private CodeTypeDeclaration CreateClass(string entity, string ns, CodeCompileUnit targetUnit)
        {
            CodeNamespace webAppModelsNs = new CodeNamespace(ns);
            webAppModelsNs.Imports.Add(new CodeNamespaceImport("System"));
            webAppModelsNs.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            var entityClass = new CodeTypeDeclaration(entity)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };
            webAppModelsNs.Types.Add(entityClass);
            targetUnit.Namespaces.Add(webAppModelsNs);
            return entityClass;
        }

        private CodeMemberProperty CreateProperty(string name, string type)
        {
            return new CodeMemberProperty
            {
                Attributes =
                MemberAttributes.Public | MemberAttributes.Final,
                Name = name,
                HasGet = true,
                HasSet = true,
                Type = new CodeTypeReference(type)
            };
        }

        private static void GenerateCSharpCode(string fileName, string folder, CodeCompileUnit targetUnit)
        {
            if (!Directory.Exists(folder))  // if it doesn't exist, create
                Directory.CreateDirectory(folder);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions
            {
                BracingStyle = "C"
            };
            using (StreamWriter sourceWriter = new StreamWriter(Path.Combine(folder, fileName)))
            {
                provider.GenerateCodeFromCompileUnit(
                    targetUnit, sourceWriter, options);
            }
        }
    }
}
