using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EdmxParser
{
    public class EdmxConverter
    {

        public static IEnumerable<EntityType> ParseEntites(XDocument root)
        {
            var parsedEntities = new List<EntityType>();            
            XNamespace ns = "http://schemas.microsoft.com/ado/2009/11/edm";
            var entityTypes = root.Descendants(ns + "EntityType");
            var entites = entityTypes.ToList();
            var association = ParseAssociations(root);
            foreach (var entity in entites)
            {
                var props = entity.Descendants(ns + "Property")
                    .Select(x => x.ToProperty());
                var navProps = entity.Descendants(ns + "NavigationProperty")
                    .Select(x => x.ToNavigationProperty(
                        GetMultiplicity(x, association),
                        GetNavPropertyIdType(x, ns, entites)
                      )
                   );

                parsedEntities.Add(
                    new EntityType(
                            entity.AttributeValue("Name"),
                            props,
                            navProps,
                            association.Where(a => navProps.Any(n => n.Relationship.Contains(a.Name)))
                        )
                    );
            }

            return parsedEntities;
        }

        private static string GetMultiplicity(XElement navPropElement, IEnumerable<Association> association)
        {
            return association
                .FirstOrDefault(a => navPropElement.AttributeValue("Relationship").Contains(a.Name))
                .Ends
                .FirstOrDefault(e => e.Role == navPropElement.AttributeValue("ToRole")).Multiplicity;
        }

        private static string GetNavPropertyIdType(XElement navPropElement, XNamespace ns, List<XElement> entites)
        {
            return entites
                .FirstOrDefault(e => e.AttributeValue("Name") == navPropElement.AttributeValue("ToRole"))
                .Descendants(ns + "Property")
                .FirstOrDefault(p => p.AttributeValue("Name") == "Id")?
                .AttributeValue("Type") ?? null;
        }

        public static IEnumerable<Association> ParseAssociations(XDocument root)
        {
            XNamespace ns = "http://schemas.microsoft.com/ado/2009/11/edm";
            return root.Descendants(ns + "Association")
                .Select(e=>e.ToAssociation())
                .ToList();
        }
    }
}
