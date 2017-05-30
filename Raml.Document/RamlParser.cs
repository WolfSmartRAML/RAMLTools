using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raml.Parser.Builders;
using Raml.Parser.Expressions;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Raml.Parser
{
    public class RamlParser
    {
        public async Task<RamlDocument> LoadAsync(string filePath)
        {
            var rawresult = await GetDynamicStructure(filePath);

            var ramlDocument = await new RamlBuilder().Build(rawresult, filePath);

            return ramlDocument;
        }

        public static async Task<IDictionary<string, object>> GetDynamicStructure(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath");
            /*
                        var load = Edge.Func(@"

                            return function (filepath, callback) {

                                var raml1Parser = require('raml-1-0-parser');
                                var path = require('path');

                                var api = raml1Parser.loadApiSync(filepath);

                                var ret = { raml: api.toJSON(), errors: api.errors() }
                                callback(null, ret)

                            }
                        ");
            */
            JObject rawresult = null;

            var ser = new JsonSerializer()
            {

            };
            using (var sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            using (var jr = new JsonTextReader(sr))
            {
                rawresult = ser.Deserialize<JObject>(jr);
            }
            
            var dict = rawresult.ToDictionary();

            return dict;
        }

        //private static JObject GetRaml(JObject rawresult)
        //{
        //    //var error = rawresult as string;
        //    //if (!string.IsNullOrWhiteSpace(error) && error.ToLowerInvariant().Contains("error"))
        //    //    throw new FormatException(error);
        //    //
        //    //var ret = rawresult as IDictionary<string, object>;
        //    //
        //    //HandleErrors(ret);
        //
        //    return rawresult["raml"];
        //}

        private static void HandleErrors(IDictionary<string, object> ret)
        {
            if (ret == null)
                throw new FormatException("Error while parsing RAML");

            var errorsRaw = ret["errors"];

            var errorObjects = errorsRaw as object[];
            if (errorObjects != null && errorObjects.Length != 0)
            {
                var errorsBuilder = new ErrorsBuilder(errorObjects);
                var errors = errorsBuilder.GetErrors();
                if (errors.Any(e => e.IsWarning == false))
                    throw new FormatException(errorsBuilder.GetMessages());
            }
        }


        public async Task<RamlDocument> LoadAsync(string filePath, string[] extensionPaths)
        {
            return await LoadAsync(filePath);

            //if (string.IsNullOrWhiteSpace(filePath))
            //    throw new ArgumentException("filePath");

            //var load = Edge.Func(@"

            //    return function (obj, callback) {

            //        var raml1Parser = require('raml-1-0-parser');
            //        var path = require('path');

            //        var api = raml1Parser.loadApiSync(obj.Filepath, obj.Extensions);

            //        var ret = { raml: api.toJSON(), errors: api.errors() }
            //        callback(null, ret)
            //    }
            //");

            //var rawresult = await load(new { Filepath = filePath, Extensions = extensionPaths });
            //var raml = GetRaml(rawresult);
            //var ramlDocument = await new RamlBuilder().Build(raml, filePath);

            //return ramlDocument;
        }
    }
}
