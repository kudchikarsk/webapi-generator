using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;
using EdmxParser;
using Generator.DefaultRegistryGenerator;
using Generator.ModelGenerator;
using Generator.ControllerGenerator;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectPath = @"D:\Test\TestWebSolution\WebApplication\WebApplication\";
            var csProjPath = Path.Combine(projectPath, @"WebApplication.csproj");            
            var proj = XDocument.Load(csProjPath);
            var filename = "DummyModel.edmx";
            var currentDirectory = Directory.GetCurrentDirectory();
            var dataModelFilePath = Path.Combine(currentDirectory, filename);
            var file = File.ReadAllText(dataModelFilePath);
            XDocument root = XDocument.Load(dataModelFilePath);
            var entities = EdmxConverter.ParseEntites(root);
            var generators = new List<IGenerator>() {
                new BaseModelGenerator(proj),
                new BaseControllerGenerator(proj)
            };

            var entitesGenerator = new List<IEntitiesGenerator>()
            {
                new BaseDefaultRegistryGenerator(proj)
            };

            foreach (var entity in entities)
            {
                foreach (var generator in generators)
                {
                    generator.Generate(entity, projectPath);
                }
            }

            foreach (var generator in entitesGenerator)
            {
                generator.Generate(entities, projectPath);
            }

            proj.Save(csProjPath);
            Console.WriteLine("Program terminated!");
            Console.ReadKey();
        }
    }
}
