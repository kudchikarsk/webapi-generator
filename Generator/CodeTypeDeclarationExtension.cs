using System.CodeDom;

namespace Generator
{
    public static class CodeTypeDeclarationExtension
    {
        public static CodeTypeDeclaration AddMember(this CodeTypeDeclaration codeTypeDeclaration, CodeTypeMember codeTypeMember)
        {
            codeTypeDeclaration.Members.Add(codeTypeMember);
            return codeTypeDeclaration;
        }
    }
}
