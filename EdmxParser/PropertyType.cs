namespace EdmxParser
{
    public abstract class BaseProperty
    {
        public BaseProperty(string name)
        {
            Name = name;
        }

        public string Name { get;}
    }

    public class Property : BaseProperty
    {
        public Property(string name, string type, bool nullabel) : base(name)
        {
            Nullable = nullabel;
            Type = type;
        }

        public bool Nullable { get; }
        public string Type { get; }
    }

    public class NavigationProperty : BaseProperty
    {
        public NavigationProperty(string name, string fromRole, string toRole, string relationship, string multiplicity, string toRoleKeyType) : base(name)
        {
            FromRole = fromRole;
            ToRole = toRole;
            Relationship = relationship;
            Multiplicity = multiplicity;
            ToRoleKeyType = toRoleKeyType;
        }

        public string FromRole { get; } 
        public string ToRole   { get; }
        public string Relationship { get; }
        public string Multiplicity { get; }
        public string ToRoleKeyType { get; }
    }
}