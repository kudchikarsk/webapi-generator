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
                    .Select(x=>x.ToProperty());
                var navProps = entity.Descendants(ns + "NavigationProperty")
                    .Select(x => x.ToNavigationProperty(
                        association
                        .FirstOrDefault(a=> x.AttributeValue("Relationship").Contains(a.Name))
                        .Ends
                        .FirstOrDefault(e=>e.Role==x.AttributeValue("ToRole")).Multiplicity
                      )
                   );

                parsedEntities.Add(
                    new EntityType(
                            entity.AttributeValue("Name"),
                            props,
                            navProps,
                            association.Where(a=> navProps.Any(n=>n.Relationship.Contains(a.Name)))
                        )
                    );
            }

            return parsedEntities;
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
