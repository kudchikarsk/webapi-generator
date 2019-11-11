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
using Microsoft.Build.Evaluation;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectPath = @"D:\Test\TestWebSolution\WebApplication\WebApplication\WebApplication.csproj";            
            var proj = new Project(projectPath, null, "4.0");

            var filename = "DummyModel.edmx";
            var currentDirectory = Directory.GetCurrentDirectory();
            var dataModelFilePath = Path.Combine(currentDirectory, filename);
            var file = File.ReadAllText(dataModelFilePath);
            XDocument root = XDocument.Load(dataModelFilePath);
            var entites = EdmxConverter.ParseEntites(root);
            var generators = new List<IGenerator>() {
                new DataModelGenerator(proj),
                new ViewModelGenerator(proj)
            };
            foreach (var entity in entites)
            {
                foreach (var generator in generators)
                {
                    generator.Generate(entity);
                }
            }


            proj.Save();
            Console.WriteLine("Program terminated!");
            Console.ReadKey();
        }
    }
}
