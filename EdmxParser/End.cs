namespace EdmxParser
{
    public class End
    {
        public End
            (
            string type         ,
            string role         ,
            string multiplicity 
            )
        {
            Type         = type         ;
            Role         = role         ;
            Multiplicity = multiplicity ;
        }

        public string Type         { get ; } 
        public string Role         { get ; } 
        public string Multiplicity { get ; } 
    }
}