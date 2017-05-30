﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Raml.Parser.Expressions;
using RAML.APITools.Common;
using RAML.APITools.Tools.Raml.Tools;

namespace RAML.APITools.Tools.WebApiGenerator
{
    public class ModelsGeneratorService : GeneratorServiceBase
    {
        public ModelsGeneratorService(RamlDocument raml, string targetNamespace) : base(raml, targetNamespace)
        {
        }

        public ModelsGeneratorModel BuildModel()
        {
            classesNames = new Collection<string>();
            warnings = new Dictionary<string, string>();
            enums = new Dictionary<string, ApiEnum>();

            var ns = string.IsNullOrWhiteSpace(raml.Title) ? targetNamespace : NetNamingMapper.GetNamespace(raml.Title);

            new RamlTypeParser(raml.Types, schemaObjects, ns, enums, warnings).Parse();

            ParseSchemas();
            schemaRequestObjects = GetRequestObjects();
            schemaResponseObjects = GetResponseObjects();

            return new ModelsGeneratorModel
            {
                SchemaObjects = schemaObjects,
                RequestObjects = schemaRequestObjects,
                ResponseObjects = schemaResponseObjects,
                Warnings = warnings,
                Enums = Enums
            };
        }

    }
}
