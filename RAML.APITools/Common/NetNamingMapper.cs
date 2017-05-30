using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RAML.APITools.Common
{
    public class NetNamingMapper
    {
        private static readonly string[] ReservedWords = new string[7]
        {
            "Get",
            "Post",
            "Put",
            "Delete",
            "Options",
            "Head",
            "ApiClient"
        };

        public static string GetNamespace(string input)
        {
            input = NetNamingMapper.ReplaceSpecialChars(input, "-");
            return NetNamingMapper.Capitalize(NetNamingMapper.RemoveIndalidChars(input));
        }

        public static string GetVersionName(string input)
        {
            input = input.Replace(".", "_");
            input = NetNamingMapper.RemoveIndalidChars(input);
            input = input.Replace("+", string.Empty);
            input = NetNamingMapper.Capitalize(input);
            if (NetNamingMapper.StartsWithNumber(input))
                input = "V" + input;
            return input;
        }

        public static string GetObjectName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "NullInput";
            string name = NetNamingMapper.RemoveIndalidChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(input, "{mediaTypeExtension}"), "-"), "\\"), "/"), "_"), ":"), "("), ")"), "'"), "`"), "{"), "}"));
            if (((IEnumerable<string>)NetNamingMapper.ReservedWords).Contains<string>(name))
                name += "Object";
            if (NetNamingMapper.StartsWithNumber(name))
                name = "O" + name;
            return name;
        }

        private static string ReplaceSpecialChars(string key, string separator)
        {
            return NetNamingMapper.ReplaceSpecialChars(key, new string[1]
            {
                separator
            });
        }

        private static string ReplaceSpecialChars(string key, string[] separator)
        {
            string empty = string.Empty;
            return ((IEnumerable<string>)key.Split(separator, StringSplitOptions.RemoveEmptyEntries)).Aggregate<string, string>(empty, (Func<string, string, string>)((current, word) => current + NetNamingMapper.Capitalize(word)));
        }

        public static string Capitalize(string word)
        {
            return word.Substring(0, 1).ToUpper() + word.Substring(1);
        }

        public static string RemoveIndalidChars(string input)
        {
            return ((IEnumerable<char>)Path.GetInvalidPathChars()).Aggregate<char, string>(input, (Func<string, char, string>)((current, invalidChar) => current.Replace(invalidChar.ToString(), string.Empty))).Replace(" ", string.Empty).Replace(".", string.Empty).Replace("?", string.Empty).Replace("[]", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("|", string.Empty);
        }

        public static bool HasIndalidChars(string input)
        {
            return input.IndexOfAny(Path.GetInvalidPathChars()) >= 0;
        }

        public static string GetMethodName(string input)
        {
            string name = NetNamingMapper.RemoveIndalidChars(NetNamingMapper.ReplaceUriParameters(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(NetNamingMapper.ReplaceSpecialChars(input, "{mediaTypeExtension}"), "-"), "\\"), "/"), "_"), "("), ")"), "'"), "`")).Replace(":", string.Empty));
            if (NetNamingMapper.StartsWithNumber(name))
                name = "M" + name;
            return name;
        }

        private static bool StartsWithNumber(string name)
        {
            return new Regex("^[0-9]+").IsMatch(name);
        }

        private static string ReplaceUriParameters(string input)
        {
            if (!input.Contains("{"))
                return input;
            input = input.Substring(0, input.IndexOf("{", StringComparison.Ordinal)) + "By" + input.Substring(input.IndexOf("{", StringComparison.Ordinal));
            string empty = string.Empty;
            return ((IEnumerable<string>)input.Split(new string[2]
            {
                "{",
                "}"
            }, StringSplitOptions.RemoveEmptyEntries)).Aggregate<string, string>(empty, (Func<string, string, string>)((current, word) => current + NetNamingMapper.Capitalize(word)));
        }

        public static string GetPropertyName(string name)
        {
            string name1 = NetNamingMapper.Capitalize(name.Replace(":", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty).Replace("`", string.Empty).Replace("?", string.Empty).Replace("[]", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("|", string.Empty).Replace("+", "Plus").Replace(".", "Dot"));
            if (NetNamingMapper.StartsWithNumber(name1))
                name1 = "P" + name1;
            return name1;
        }

        public static string GetEnumValueName(string enumValue)
        {
            string name = enumValue.Replace(":", string.Empty).Replace("/", string.Empty).Replace(" ", "_").Replace("-", "_").Replace("+", string.Empty).Replace(".", string.Empty);
            if (NetNamingMapper.StartsWithNumber(name))
                name = "E" + name;
            int result;
            if (int.TryParse(enumValue, out result))
                name = name + " = " + (object)result;
            return name;
        }
    }
}
