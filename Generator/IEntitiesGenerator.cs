using System.Collections.Generic;
using EdmxParser;

namespace Generator
{
    internal interface IEntitiesGenerator
    {
        void Generate(IEnumerable<EntityType> entites, string projectPath);
    }
}