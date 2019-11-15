using System.Linq;
using System.Xml.Linq;

namespace EdmxParser
{
    public static class XElementExtension
    {
        public static Property ToProperty(this XElement xElement)
        {
            return new Property(
                xElement.AttributeValue("Name"),
                xElement.AttributeValue("Type"),
                xElement.AttributeValue<bool>("Nullable")
                );
        }

        public static NavigationProperty ToNavigationProperty(this XElement xElement,string multiplicity, string toRoleKeyType)
        {
            return new NavigationProperty(
                xElement.AttributeValue("Name"),
                xElement.AttributeValue("FromRole"),
                xElement.AttributeValue("ToRole"),
                xElement.AttributeValue("Relationship"),
                multiplicity,
                toRoleKeyType
                );
        }

        public static Association ToAssociation(this XElement xElement)
        {
            return new Association(
                xElement.AttributeValue("Name"),
                xElement.Descendants().Select(e=>e.ToEnd()).ToList()
                );
        }

        public static End ToEnd(this XElement xElement)
        {
            return new End(
                xElement.AttributeValue("Type"),
                xElement.AttributeValue("Role"),
                xElement.AttributeValue("Multiplicity")
                );
        }
    }
}
