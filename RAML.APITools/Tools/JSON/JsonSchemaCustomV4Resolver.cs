using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Schema;

namespace RAML.APITools.Tools.JSON
{
    public class JsonSchemaCustomV4Resolver : JSchemaResolver
    {
        private readonly IDictionary<string, ApiObject> objects;

        public JsonSchemaCustomV4Resolver(IDictionary<string, ApiObject> objects)
        {
            this.objects = objects;
        }

        // AKSTODO no GetSchema() to override
        //public override JsonSchema GetSchema(string reference)
        //{
        //    var schema = base.GetSchema(reference);

        //    if (schema != null) return schema;

        //    if (!objects.ContainsKey(reference))
        //        return null;

        //    var jsonSchema = objects[reference].JSONSchema.Replace("\\\"", "\"");
        //    return JsonSchema.Parse(jsonSchema, new JsonSchemaCustomV4Resolver(objects));
        //}

        // AKSTODO required
        public override Stream GetSchemaResource(ResolveSchemaContext context, SchemaReference reference)
        {
            throw new NotImplementedException();
        }

    }
}
