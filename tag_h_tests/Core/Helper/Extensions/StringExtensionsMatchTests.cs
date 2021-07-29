using tag_h.Core.Helper.Extensions;

using NUnit.Framework;
using FluentAssertions;

namespace tag_h_tests.Core.Helper.Extensions
{
    public class StringExtensionsMatchTests
    {
        [Test]
        public void MatchFirst_WithCorrectMatches_MatchesCorrectly()
        {
            AssertMatchSuccess("EasyExampleThing", @"(Example)", "Example");
            AssertMatchSuccess("HttpMethodAttribute", @"Http(.*)Attribute", "Method");
            AssertMatchSuccess("ApiControllerBinderController", @"(.*)Controller$", "ApiControllerBinder");
        }

        [Test]
        public void MatchFirst_WithIncorrectMatches_ThrowsMatchFirstFailed()
        {
            AssertMatchFailed("EasyBxampleThing", @"(Example)");
        }

        static private void AssertMatchSuccess(string input, string regex, string expected)
        {
            input.MatchFirst(regex).Should().Be(expected);
        }

        static private void AssertMatchFailed(string input, string regex)
        {
            input.Invoking(value => value.MatchFirst(regex)).Should().Throw<MatchFirstFailed>();
        }
    }
}