namespace DS.LabCodes.Tests
{
    [TestFixture]
    public class JsonParserPatternMatchingTests
    {
        [Test]
        public void Parse_ValidJsonObject_ReturnsCorrectDictionary()
        {
            string json = @"{ ""name"": ""Alice"", ""age"": 30, ""isStudent"": false }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result["name"], Is.EqualTo("Alice"));
            Assert.That(result["age"], Is.EqualTo(30));
            Assert.That(result["isStudent"], Is.EqualTo(false));
        }

        [Test]
        public void Parse_ValidJsonArray_ReturnsCorrectList()
        {
            string json = @"[1, 2, 3, 4, 5]";
            var result = (List<object>)JsonParserPatternMatching.Parse(json);

            CollectionAssert.AreEqual(new List<object> { 1, 2, 3, 4, 5 }, result);
        }

        [Test]
        public void Parse_NestedJsonObject_ReturnsCorrectStructure()
        {
            string json = @"{ ""person"": { ""name"": ""Bob"", ""age"": 25 } }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);
            var person = (Dictionary<string, object>)result["person"];

            Assert.That(person["name"], Is.EqualTo("Bob"));
            Assert.That(person["age"], Is.EqualTo(25));
        }

        [Test]
        public void Parse_NestedJsonArray_ReturnsCorrectStructure()
        {
            string json = @"{ ""numbers"": [10, 20, 30] }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);
            var numbers = (List<object>)result["numbers"];

            CollectionAssert.AreEqual(new List<object> { 10, 20, 30 }, numbers);
        }

        [Test]
        public void Parse_StringWithEscapeCharacters_ReturnsCorrectString()
        {
            string json = @"{ ""message"": ""Hello\nWorld!"" }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result["message"], Is.EqualTo("Hello\nWorld!"));
        }

        [Test]
        public void Parse_BooleanValues_ReturnsCorrectBooleans()
        {
            string json = @"{ ""isTrue"": true, ""isFalse"": false }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result["isTrue"], Is.EqualTo(true));
            Assert.That(result["isFalse"], Is.EqualTo(false));
        }

        [Test]
        public void Parse_NullValue_ReturnsNull()
        {
            string json = @"{ ""data"": null }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.IsNull(result["data"]);
        }

        [Test]
        public void Parse_InvalidJson_ThrowsException()
        {
            string json = @"{ ""name"": ""Alice"", ""age"": 30, ";

            var ex = Assert.Throws<Exception>(() => JsonParserPatternMatching.Parse(json));
            Assert.That(ex.Message, Does.Contain("Unexpected end of JSON"));

        }

        [Test]
        public void Parse_NumberTypes_ReturnsCorrectTypes()
        {
            string json = @"{ ""intValue"": 100, ""floatValue"": 99.99 }";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result["intValue"], Is.EqualTo(100));
            Assert.That((double)result["floatValue"], Is.EqualTo(99.99).Within(0.0001));
        }

        [Test]
        public void Parse_EmptyObject_ReturnsEmptyDictionary()
        {
            string json = @"{}";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Parse_EmptyArray_ReturnsEmptyList()
        {
            string json = @"[]";
            var result = (List<object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Parse_WhitespaceInJson_ParsesCorrectly()
        {
            string json = @" {  ""key""   :   ""value""  ,  ""number"" :  42  } ";
            var result = (Dictionary<string, object>)JsonParserPatternMatching.Parse(json);

            Assert.That(result["key"], Is.EqualTo("value"));
            Assert.That(result["number"], Is.EqualTo(42));
        }
    }
}
