using tag_h.Helper.Extensions;

using NUnit.Framework;
using FluentAssertions;


namespace tag_h_tests.Core.Helper.Extensions
{
    public class StringExtensionsTests
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

        [Test]
        public void Filter_WithMultipleTokens_FiltersTokens()
        {
            "This is a more complicated instance of a string".Filter("more complicated", "a").Should().Be("This is   instnce of  string");
            "coffret_(heartcatch_precure!)".Filter("(", ")").Should().Be("coffret_heartcatch_precure!");
        }

        [Test]
        public void Concat_WithCharacters_ConcatenatesString()
        {
            var characters = new[] { 'a', 'b', 'c' };

            characters.Concat().Should().Be("abc");
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