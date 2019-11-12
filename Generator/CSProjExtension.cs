using System.Linq;
using System.Xml.Linq;
using EdmxParser;

namespace Generator
{
    public static class CSProjExtension
    {
        public static void AddToItemGroup(this XDocument proj, string includeValue)
        {
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var group = proj.Descendants(ns + "ItemGroup").FirstOrDefault();
            if (group == null)
            {
                group = new XElement(ns + "ItemGroup");
                proj.Root.Add(group);
            }

            if (proj.Descendants(ns + "Compile").Any(d => d.AttributeValue("Include") == includeValue)) return;
            var element = new XElement(ns + "Compile");
            element.SetAttributeValue("Include", includeValue);
            group.Add(element);            
        }
    }
}
