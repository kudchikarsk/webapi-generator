using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public static class CodeDomHelper
    {
        public static (CodeNamespace @namespace, CodeTypeDeclaration @class) CreateClass(string entity, string ns, bool isPartial = true)
        {
            CodeNamespace @namespace = new CodeNamespace(ns);
            @namespace.Imports.Add(new CodeNamespaceImport("System"));
            @namespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            var @class = new CodeTypeDeclaration(entity)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public,
                IsPartial = isPartial
            };
            @namespace.Types.Add(@class);
            return (@namespace, @class);
        }

        public static (CodeTypeMember field, CodeTypeMember prop) CreateProperty(string name, string type)
        {
            var field = new CodeMemberField()
            {
                Name = "_" + name,
                Type = new CodeTypeReference(type),
                Attributes = MemberAttributes.Private
            };

            var prop = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = name,
                Type = new CodeTypeReference(type)
            };

            prop.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "_" + name)));
            prop.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "_" + name),
                    new CodePropertySetValueReferenceExpression()));

            

            return (field, prop);
        }

        public static void GenerateCSharpCode(string fileName, string folder, CodeCompileUnit targetUnit)
        {
            if (!Directory.Exists(folder))  // if it doesn't exist, create
                Directory.CreateDirectory(folder);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions
            {
                BracingStyle = "C"
            };
            using (StreamWriter sourceWriter = new StreamWriter(Path.Combine(folder, fileName)))
            {
                provider.GenerateCodeFromCompileUnit(
                    targetUnit, sourceWriter, options);
            }
        }
    }
}
