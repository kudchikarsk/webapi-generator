using EdmxParser;
using System.Collections.Generic;

namespace Generator
{
    public interface IGenerator
    {
        void Generate(EntityType entity, string projectPath);
    }
}