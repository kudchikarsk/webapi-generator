using System.Collections.Generic;

namespace EdmxParser
{
    public class Association
    {
        public Association(string name, IEnumerable<End> ends)
        {
            Name = name;
            Ends = ends;
        }

        public string Name { get; }
        public IEnumerable<End> Ends { get; }
    }
}