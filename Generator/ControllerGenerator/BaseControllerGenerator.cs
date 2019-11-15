using EdmxParser;
using Generator.ControllerGenerator;
using Microsoft.VisualStudio.TextTemplating;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Scriban;
using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using static Generator.CodeDomHelper;

namespace Generator.ControllerGenerator
{
    public class BaseControllerGenerator : IGenerator
    {
        private readonly XDocument proj;

        public BaseControllerGenerator(XDocument proj)
        {
            this.proj = proj;
        }

        public void Generate(EntityType entity, string projectPath)
        {
            string templateText = ControllerGeneratorResource.ControllerTemplate; ;

            var template = Template.Parse(templateText);
            var result = template.Render(new {
                EntityName = entity.Name,
                KeyDataType = entity.Properties.First(e=>e.Name == "Id").Type,
                IsUserEntity = entity.NavigationProperties.Any(p=>p.Name == "ApplicationUser" && p.Multiplicity=="1"),
                entity.Properties
            });
            result = ArrangeUsingRoslyn(result);
            var csu = new CodeSnippetCompileUnit(result);

            var fileName = $"{entity.Name}Controller.cs";
            var folderName = "Controllers";
            var absolutePath = Path.Combine(projectPath, folderName);
            GenerateCSharpCode(fileName, absolutePath, csu);

            var relativePath = Path.Combine(folderName, fileName);
            proj.AddToItemGroup(relativePath);
        }        
    }
}