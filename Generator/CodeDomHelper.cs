﻿using System;
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
        public static (CodeNamespace @namespace, CodeTypeDeclaration @class) CreateClass(string entity, string ns)
        {
            CodeNamespace @namespace = new CodeNamespace(ns);
            @namespace.Imports.Add(new CodeNamespaceImport("System"));
            @namespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            var @class = new CodeTypeDeclaration(entity)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };
            @namespace.Types.Add(@class);
            return (@namespace, @class);
        }

        public static CodeTypeMember CreateProperty(string name, string type)
        {
            return new CodeMemberField
            {
                Attributes =
                MemberAttributes.Public | MemberAttributes.Final,
                Name = name + " { get; set; }",
                Type = new CodeTypeReference(type)
            };
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
