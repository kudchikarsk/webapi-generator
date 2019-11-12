using System.CodeDom;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using EdmxParser;
using Generator.ModelGenerator;
using Scriban;
using static Generator.CodeDomHelper;

namespace Generator.ModelGenerator
{
    public class BaseModelGenerator : IGenerator
    {
        private readonly XDocument proj;

        public BaseModelGenerator(XDocument proj)
        {
            this.proj = proj;
        }

        public void Generate(EntityType entity, string projectPath)
        {
            string templateText = ModelGeneratorResource.ModelTemplate;

            var template = Template.Parse(templateText);
            var result = template.Render(new { EntityName = entity.Name, entity.Properties, NavProperties = entity.NavigationProperties });
            result = ArrangeUsingRoslyn(result);
            var csu = new CodeSnippetCompileUnit(result);

            var fileName = $"{entity.Name}.cs";
            var folderName = "Models";
            var absolutePath = Path.Combine(projectPath, folderName);
            GenerateCSharpCode(fileName, absolutePath, csu);

            var relativePath = Path.Combine(folderName, fileName);
            proj.AddToItemGroup(relativePath);
        }
    }
}
