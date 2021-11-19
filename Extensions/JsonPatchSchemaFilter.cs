using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JsonPatchSample
{
    public class JsonPatchSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Microsoft.AspNetCore.JsonPatch.Operations.Operation))
            {
                OpenApiArray openApiArray = new();

                foreach (var item in GetExamples())
                {
                    openApiArray.Add(item);
                }

                schema.Example = openApiArray;
            }
        }

        private static OpenApiObject[] GetExamples()
        {
            return new OpenApiObject[]
            {
                new OpenApiObject
                {
                    ["op"] =new OpenApiString("replace"),
                    ["path"] = new OpenApiString("/property"),
                    ["value"] = new OpenApiString("New Value")
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("add"),
                    ["path"] = new OpenApiString("/property"),
                    ["value"] = new OpenApiString("New Value")
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("remove"),
                    ["path"] = new OpenApiString("/property"),
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("copy"),
                    ["from"] =new OpenApiString("/fromProperty"),
                    ["path"] =new OpenApiString("/toProperty"),
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("move"),
                    ["from"] =new OpenApiString("/fromProperty"),
                    ["path"] =new OpenApiString("/toProperty"),
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("test"),
                    ["path"] = new OpenApiString("/property"),
                    ["value"] = new OpenApiString("Has Value")
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("replace"),
                    ["path"] = new OpenApiString("/arrayProperty/0"),
                    ["value"] = new OpenApiString("Replace First Array Item")
                },
                new OpenApiObject()
                {
                    ["op"] =new OpenApiString("replace"),
                    ["path"] = new OpenApiString("/arrayProperty/-"),
                    ["value"] = new OpenApiString("Replace Last Array Item")
                }
            };
        }
    }
}