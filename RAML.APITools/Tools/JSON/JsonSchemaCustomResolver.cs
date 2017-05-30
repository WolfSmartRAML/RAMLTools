using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Schema;

namespace RAML.APITools.Tools.JSON
{
    public class JsonSchemaCustomResolver : JSchemaResolver
    {
        private readonly IDictionary<string, ApiObject> objects;
        private readonly IDictionary<string, ApiObject> schemaObjects;

        public JsonSchemaCustomResolver(IDictionary<string, ApiObject> objects, IDictionary<string, ApiObject> schemaObjects)
        {
            this.objects = objects;
            this.schemaObjects = schemaObjects;
        }

        // AKSTODO no GetSchema() to override
        //public override JSchema GetSchema(string reference)
        //{
        //    var schema = base.GetSchema(reference);

        //    if (schema != null) return schema;

        //    if (!objects.ContainsKey(reference) && !schemaObjects.ContainsKey(reference))
        //        return null;

        //    string jsonSchema;

        //    if (objects.ContainsKey(reference))
        //    {
        //        jsonSchema = objects[reference].JSONSchema.Replace("\\\"", "\"");
        //    }
        //    else
        //    {
        //        jsonSchema = schemaObjects[reference].JSONSchema.Replace("\\\"", "\"");
        //    }
        //    return JSchema.Parse(jsonSchema, new JsonSchemaCustomResolver(objects, schemaObjects));
        //}

        // AKSTODO required
        public override Stream GetSchemaResource(ResolveSchemaContext context, SchemaReference reference)
        {
            throw new NotImplementedException();
        }
    }
}
