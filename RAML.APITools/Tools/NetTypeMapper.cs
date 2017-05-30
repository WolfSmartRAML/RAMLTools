using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json.Schema;

namespace RAML.APITools.Tools
{
    public class NetTypeMapper
    {
        private static readonly IDictionary<JSchemaType, string> TypeConversion =
            new Dictionary<JSchemaType, string>
            {
                {
                    JSchemaType.Integer,
                    "int"
                },
                {
                    JSchemaType.String,
                    "string"
                },
                {
                    JSchemaType.Boolean,
                    "bool"
                },
                {
                    JSchemaType.Number,
                    "decimal"
                },
                {
                    JSchemaType.Object,
                    "object"
                }
            };

        private static readonly IDictionary<string, string> TypeStringConversion =
            new Dictionary<string, string>
            {
                {
                    "integer",
                    "int"
                },
                {
                    "string",
                    "string"
                },
                {
                    "boolean",
                    "bool"
                },
                {
                    "float",
                    "decimal"
                },
                {
                    "number",
                    "decimal"
                },
                {
                    "any",
                    "object"
                },
                {
                    "date",
                    "DateTime"
                },
                {
                    "datetime",
                    "DateTime"
                },
                {
                    "date-only",
                    "DateTime"
                },
                {
                    "time-only",
                    "DateTime"
                },
                {
                    "datetime-only",
                    "DateTime"
                },
                {
                    "file",
                    "byte[]"
                }
            };

        private static readonly IDictionary<string, string> NumberFormatConversion = new Dictionary<string, string>
        {
            {"double", "double"},
            {"float", "float"},
            {"int16", "short"},
            {"short", "short"},
            {"int64", "long"},
            {"long", "long"},
            {"int32", "int"},
            {"int", "int"},
            {"int8", "byte"}
        };

        private static readonly IDictionary<string, string> DateFormatConversion = new Dictionary<string, string>
        {
            {"rfc3339", "DateTime"},
            {"rfc2616", "DateTimeOffset"}
        };

        public static string GetNetType(string type, string format)
        {
            string netType;
            if (!string.IsNullOrWhiteSpace(format) &&
                (NumberFormatConversion.ContainsKey(format.ToLowerInvariant()) || DateFormatConversion.ContainsKey(format.ToLowerInvariant())))
            {
                netType = NumberFormatConversion.ContainsKey(format.ToLowerInvariant())
                    ? NumberFormatConversion[format.ToLowerInvariant()]
                    : DateFormatConversion[format.ToLowerInvariant()];
            }
            else
            {
                netType = Map(type);
            }
            return netType;
        }

        public static string GetNetType(JSchemaType? jsonSchemaType, string format)
        {
            string netType;
            if (!string.IsNullOrWhiteSpace(format) &&
                (NumberFormatConversion.ContainsKey(format.ToLowerInvariant()) || DateFormatConversion.ContainsKey(format.ToLowerInvariant())))
            {
                netType = NumberFormatConversion.ContainsKey(format.ToLowerInvariant())
                    ? NumberFormatConversion[format.ToLowerInvariant()]
                    : DateFormatConversion[format.ToLowerInvariant()];
            }
            else
            {
                netType = Map(jsonSchemaType);
            }
            return netType;
        }

        private static string Map(JSchemaType? type)
        {
            return type == null || !TypeConversion.ContainsKey(type.Value) ? null : TypeConversion[type.Value];
        }

        public static string Map(string type)
        {
            if (type != null)
                type = type.Trim();

            return !TypeStringConversion.ContainsKey(type) ? null : TypeStringConversion[type];
        }

        private static readonly string[] OtherPrimitiveTypes = { "double", "float", "byte", "short", "long", "DateTimeOffset" };

        public static bool IsPrimitiveType(string type)
        {
            type = type.Trim();

            if (type.EndsWith("?"))
                type = type.Substring(0, type.Length - 1);

            if (OtherPrimitiveTypes.Contains(type))
                return true;

            return TypeStringConversion.Any(t => t.Value == type) || TypeStringConversion.ContainsKey(type);
        }

        public static string Map(XmlQualifiedName schemaTypeName)
        {
            return schemaTypeName.Name;
        }
    }
}
