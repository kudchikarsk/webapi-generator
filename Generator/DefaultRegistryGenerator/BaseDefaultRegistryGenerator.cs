using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EdmxParser;
using Scriban;
using static Generator.CodeDomHelper;

namespace Generator.DefaultRegistryGenerator
{
    public class BaseDefaultRegistryGenerator : IEntitiesGenerator
    {
        private readonly XDocument proj;

        public BaseDefaultRegistryGenerator(XDocument proj)
        {
            this.proj = proj;
        }

        public void Generate(IEnumerable<EntityType> entities, string projectPath)
        {
            string templateText = DefaultRegistryResource.DefaultRegistryTemplate;

            var template = Template.Parse(templateText);
            var result = template.Render(new { entities });
            result = ArrangeUsingRoslyn(result);
            var csu = new CodeSnippetCompileUnit(result);

            var fileName = "DefaultRegistry.cs";
            var folderName = "DependencyResolution";
            var absolutePath = Path.Combine(projectPath, folderName);
            GenerateCSharpCode(fileName, absolutePath, csu);
        }
    }
}
