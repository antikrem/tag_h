using tag_h.Core.Helper.Extensions;

using NUnit.Framework;
using FluentAssertions;

namespace tag_h_tests
{
    public class Tests
    {
        private Jsonifier _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Jsonifier();
        }

        [Test]
        public void Jsonify_WithString_ProperlyConverts()
        {
            var result = _sut.Jsonify("FooBar");
            result.Should().Equals("\"FooBar\"");
        }

        [Test]
        public void Jsonify_WithAnonymousObject_ProperlyConverts()
        {
            var result = _sut.Jsonify(new { Name = "Foo", Age = 42 });
            result.Should().Equals("{\"Name\":\"Foo\",\"Age\":42}");
        }

        [Test]
        public void Jsonify_WithConcreteObject_ProperlyConverts()
        {
            var result = _sut.Jsonify(new TestObject { Name = "Foo", Age = 42 });
            result.Should().Equals("{\"Name\":\"Foo\",\"Age\":42}");
        }

        [Test]
        public void ParseJson_WithString_ProperlyConverts()
        {
            var result = _sut.ParseJson<string>("\"FooBar\"");
            result.Should().Equals("FooBar");
        }

        [Test]
        public void ParseJson_WithConcreteObject_ProperlyConverts()
        {
            var result = _sut.ParseJson<TestObject>("{\"Name\":\"Foo\",\"Age\":42}");
            result.Should().BeEquivalentTo(new TestObject { Name = "Foo", Age = 42 });
        }

        private class TestObject
        {
            public string Name { get; set; }
            public int Age { get; set; }

        }
    }
}