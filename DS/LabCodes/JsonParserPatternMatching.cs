using System.Globalization;
using System.Text;

namespace DS.LabCodes
{
    public class JsonParserPatternMatching
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

            return json[index] switch  // 패턴 매칭 적용!
            {
                '{' => ParseObject(json, ref index),
                '[' => ParseArray(json, ref index),
                '"' => ParseString(json, ref index),
                '-' or >= '0' and <= '9' => ParseNumber(json, ref index), // 숫자 처리 패턴 매칭
                _ => json.Substring(index) switch
                {
                    var s when s.StartsWith("true") => ParseLiteral(json, ref index, "true", true),
                    var s when s.StartsWith("false") => ParseLiteral(json, ref index, "false", false),
                    var s when s.StartsWith("null") => ParseLiteral(json, ref index, "null", null),
                    _ => throw new Exception($"Unexpected character at index {index}: {json[index]}")
                }
            };
        }

        private static Dictionary<string, object> ParseObject(string json, ref int index)
        {
            Dictionary<string, object> result = new();
            index++; // '{' 건너뛰기
            SkipWhitespace(json, ref index);

            while (true)
            {
                if (index >= json.Length)
                    throw new Exception("Unexpected end of JSON while parsing object");

                switch (json[index]) // 패턴 매칭 적용
                {
                    case '}': // 객체 닫힘
                        index++;
                        return result;

                    case ',':
                        index++; // ',' 건너뛰기
                        SkipWhitespace(json, ref index);
                        continue;
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

                // ',' 또는 '}'만 허용
                switch (json[index])
                {
                    case ',':
                        index++; // ',' 건너뛰기
                        SkipWhitespace(json, ref index);
                        continue;

                    case '}': // 객체 닫힘
                        index++;
                        return result;

                    default:
                        throw new Exception($"Unexpected character '{json[index]}' while parsing object");
                }
            }
        }

        private static List<object> ParseArray(string json, ref int index)
        {
            List<object> result = new();
            index++; // '[' 건너뛰기
            SkipWhitespace(json, ref index);

            while (true)
            {
                if (index >= json.Length)
                    throw new Exception("Unexpected end of JSON while parsing array");

                switch (json[index])
                {
                    case ']': // 배열 닫힘
                        index++;
                        return result;

                    case ',':
                        index++; // ',' 건너뛰기
                        SkipWhitespace(json, ref index);
                        continue;
                }

                object value = ParseValue(json, ref index);
                result.Add(value);

                SkipWhitespace(json, ref index);

                if (index >= json.Length)
                    throw new Exception("Unexpected end of JSON while expecting ',' or ']'");

                switch (json[index])
                {
                    case ',':
                        index++; // ',' 건너뛰기
                        SkipWhitespace(json, ref index);
                        continue;

                    case ']': // 배열 닫힘
                        index++;
                        return result;

                    default:
                        throw new Exception("Expected ',' or ']' in array");
                }
            }
        }

        private static string ParseString(string json, ref int index)
        {
            StringBuilder sb = new();
            index++; // '"' 건너뛰기

            while (index < json.Length)
            {
                char current = json[index];

                switch (current) // 패턴 매칭 적용
                {
                    case '"': // 문자열 끝
                        index++;
                        return sb.ToString();

                    case '\\': // Escape 문자 처리
                        index++;
                        if (index >= json.Length) throw new Exception("Unexpected end of JSON string");

                        sb.Append(json[index] switch
                        {
                            '"' => '"',
                            '\\' => '\\',
                            '/' => '/',
                            'b' => '\b',
                            'f' => '\f',
                            'n' => '\n',
                            'r' => '\r',
                            't' => '\t',
                            _ => throw new Exception($"Invalid escape sequence \\{json[index]}")
                        });
                        break;

                    default:
                        sb.Append(current);
                        break;
                }
                index++;
            }

            throw new Exception("Unexpected end of JSON string");
        }


        private static object ParseNumber(string json, ref int index)
        {
            int start = index;

            if (json[index] == '-') index++; // 음수 처리

            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '.'))
                index++;

            string numberStr = json.Substring(start, index - start);

            return numberStr.Contains(".")
                ? double.Parse(numberStr, CultureInfo.InvariantCulture)  // 패턴 매칭 적용 필요 없음
                : int.Parse(numberStr);
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
