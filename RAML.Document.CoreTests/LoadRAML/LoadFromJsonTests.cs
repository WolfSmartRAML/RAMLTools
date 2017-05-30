using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Raml.Parser;
using Raml.Parser.Expressions;

namespace RAML.Document.Tests.LoadRAML
{
    [TestClass]
    public class LoadFromJsonTests
    {
        [TestMethod]
        public async Task A001_LoadRAML()
        {
            try
            {
                var parser = new RamlParser();
                var raml = await parser.LoadAsync("raml/sa.json");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        [TestMethod]
        public async Task A002_LoadRAML()
        {
            try
            {
                var parser = new RamlParser();
                var raml = await parser.LoadAsync("C:\\WIP\\Projects\\hmrc\\githubWS\\hmrc-api\\apis\\SA\\application.json");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        [TestMethod]
        public async Task A003_LoadRAML2Json()
        {
            try
            {
                var parser = new RamlParser();
                var raml = await parser.LoadAsync("C:\\WIP\\Projects\\hmrc\\githubWS\\hmrc-api\\apis\\SA\\application.json");

                var sb = new StringBuilder();
                var ser = new JsonSerializer()
                {
                    Formatting = Formatting.Indented
                };
                using (var jw = new JsonTextWriter(new StringWriter(sb)))
                {
                    ser.Serialize(jw, raml);
                }

                var json = sb.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public async Task B001_ListAPI()
        {
            try
            {
                var parser = new RamlParser();
                var raml = await parser.LoadAsync("C:\\WIP\\Projects\\hmrc\\githubWS\\hmrc-api\\apis\\SA\\application.json");

                var resTypes = raml.ResourceTypes.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public async Task B002_ListAPI()
        {
            try
            {
                var parser = new RamlParser();
                var raml = await parser.LoadAsync("C:\\WIP\\Projects\\hmrc\\githubWS\\hmrc-api\\apis\\SA\\application.json");

                foreach (var r in raml.Resources)
                {
                    await WalkResource(r);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        protected async Task WalkResource(Resource res)
        {
            if (res.Methods.Any())
            {
                var u = res.RelativeUri;

                foreach (var m in res.Methods)
                {
                    var x = m.Verb;
                }
            }

            if (res.Resources.Any())
            {
                foreach (var r in res.Resources)
                {
                    await WalkResource(r);
                }
            }

        }
    }
}
