using System.CodeDom;

namespace Generator
{
    public static class CodeTypeDeclarationExtension
    {
        public static CodeTypeDeclaration AddProperty(this CodeTypeDeclaration codeTypeDeclaration, CodeMemberProperty codeMemberProperty)
        {
            codeTypeDeclaration.Members.Add(codeMemberProperty);
            return codeTypeDeclaration;
        }
    }
}
