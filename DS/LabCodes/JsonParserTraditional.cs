using System;
using System.Collections.Generic;
using System.Text;

namespace DS.LabCodes
{
    public class JsonParserTraditional
    {
        public static object Parse(string json)
        {
            int index = 0;
            return ParseValue(json, ref index);
        }

        private static object ParseValue(string json, ref int index)
        {
            SkipWhitespace(json, ref index);

            if (index >= json.Length)
                throw new Exception("Unexpected end of JSON");

            char current = json[index];

            if (current == '{') return ParseObject(json, ref index);
            if (current == '[') return ParseArray(json, ref index);
            if (current == '"') return ParseString(json, ref index);
            if (char.IsDigit(current) || current == '-') return ParseNumber(json, ref index);
            if (json.Substring(index).StartsWith("true")) return ParseLiteral(json, ref index, "true", true);
            if (json.Substring(index).StartsWith("false")) return ParseLiteral(json, ref index, "false", false);
            if (json.Substring(index).StartsWith("null")) return ParseLiteral(json, ref index, "null", null);

            throw new Exception($"Unexpected character at index {index}: {current}");
        }

        private static Dictionary<string, object> ParseObject(string json, ref int index)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            index++; // '{' 건너뛰기
            SkipWhitespace(json, ref index);

            while (true)
            {
                if (index >= json.Length)
                    throw new Exception("Unexpected end of JSON while parsing object");

                if (json[index] == '}')
                {
                    index++; // '}' 건너뛰기
                    return result;
                }

                // 키 읽기
                SkipWhitespace(json, ref index);
                if (index >= json.Length) throw new Exception("Unexpected end of JSON while reading key");
                string key = ParseString(json, ref index);
                SkipWhitespace(json, ref index);

                // ':' 확인
                if (index >= json.Length || json[index] != ':')
                    throw new Exception($"Expected ':' after key \"{key}\" but got unexpected end of JSON");

                index++; // ':' 건너뛰기
                SkipWhitespace(json, ref index);

                // 값 읽기
                if (index >= json.Length)
                    throw new Exception($"Unexpected end of JSON while reading value for key \"{key}\"");

                object value = ParseValue(json, ref index);
                result[key] = value;

                SkipWhitespace(json, ref index);

                if (index >= json.Length)
                    throw new Exception("Unexpected end of JSON while expecting ',' or '}'");

                if (json[index] == ',')
                {
                    index++; // ',' 건너뛰기
                    SkipWhitespace(json, ref index);
                }
                else if (json[index] == '}')
                {
                    index++; // '}' 건너뛰기
                    return result;
                }
                else
                {
                    throw new Exception($"Unexpected character '{json[index]}' while parsing object");
                }
            }
        }

        private static List<object> ParseArray(string json, ref int index)
        {
            List<object> result = new List<object>();
            index++; // '[' 건너뛰기
            SkipWhitespace(json, ref index);

            while (json[index] != ']')
            {
                object value = ParseValue(json, ref index);
                result.Add(value);

                SkipWhitespace(json, ref index);

                if (json[index] == ',')
                {
                    index++; // ',' 건너뛰기
                    SkipWhitespace(json, ref index);
                }
                else if (json[index] != ']')
                {
                    throw new Exception("Expected ',' or ']' in array");
                }
            }
            index++; // ']' 건너뛰기
            return result;
        }

        private static string ParseString(string json, ref int index)
        {
            StringBuilder sb = new StringBuilder();
            index++; // '"' 건너뛰기

            while (index < json.Length)
            {
                char current = json[index];

                if (current == '"')
                {
                    index++; // 닫는 '"' 건너뛰기
                    return sb.ToString();
                }

                if (current == '\\')
                {
                    index++; // '\' 건너뛰기
                    if (index >= json.Length) throw new Exception("Unexpected end of JSON string");

                    current = json[index];
                    switch (current)
                    {
                        case '"': sb.Append('"'); break;
                        case '\\': sb.Append('\\'); break;
                        case '/': sb.Append('/'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'n': sb.Append('\n'); break;
                        case 'r': sb.Append('\r'); break;
                        case 't': sb.Append('\t'); break;
                        default: throw new Exception($"Invalid escape sequence \\{current}");
                    }
                }
                else
                {
                    sb.Append(current);
                }
                index++;
            }
            throw new Exception("Unexpected end of JSON string");
        }

        private static object ParseNumber(string json, ref int index)
        {
            int start = index;

            if (json[index] == '-') index++; // 음수 부호 처리

            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '.'))
            {
                index++;
            }

            string numberStr = json.Substring(start, index - start);

            if (numberStr.Contains(".")) return double.Parse(numberStr);
            return int.Parse(numberStr);
        }

        private static object ParseLiteral(string json, ref int index, string expected, object returnValue)
        {
            if (!json.Substring(index).StartsWith(expected))
                throw new Exception($"Expected '{expected}' at index {index}");

            index += expected.Length;
            return returnValue;
        }

        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index])) index++;
        }
    }
}
