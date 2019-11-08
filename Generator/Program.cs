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

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "DummyModel.edmx";
            var currentDirectory = Directory.GetCurrentDirectory();
            var dataModelFilePath = Path.Combine(currentDirectory, filename);
            var file = File.ReadAllText(dataModelFilePath);
            XDocument root = XDocument.Load(dataModelFilePath);
            var entites = EdmxConverter.ParseEntites(root);
            var generators = new List<IGenerator>() {
                new DataModelGenerator(),
                new ViewModelGenerator()
            };
            foreach (var entity in entites)
            {
                foreach (var generator in generators)
                {
                    generator.Generate(entity);
                }
            }
            Console.WriteLine("Program terminated!");
            Console.ReadKey();
        }
    }
}
