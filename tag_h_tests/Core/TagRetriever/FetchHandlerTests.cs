using System.Threading.Tasks;
using System.Collections.Generic;

using NUnit.Framework;
using NSubstitute;
using FluentAssertions;

using tag_h.Core.TagRetriever;
using tag_h.Core.Helper.Extensions;

namespace tag_h_tests.Core.TagRetriever
{
    public class FetchHandlerTests
    {
        private FetchHandler _sut;
        private IJsonifier _jsonifier;

        [SetUp]
        public void Setup()
        {
            var innerJsonifier = new Jsonifier();

            _jsonifier = Substitute.For<IJsonifier>();
            _jsonifier
                .ParseJson<List<FetchedQuote>>(Arg.Any<string>())
                .Returns(args => innerJsonifier.ParseJson<List<FetchedQuote>>((string)args[0]));

            _sut = new FetchHandler(_jsonifier);
        }

        [Test]
        public async Task FetchAsync_OnProperTarget_ReturnsExpectedResponseAsync()
        {
            var validURL = "https://animechan.vercel.app/api/quotes/character?name=okabe";

            var results = await _sut.FetchAsync<List<FetchedQuote>>(validURL);

            _jsonifier.Received().ParseJson<List<FetchedQuote>>(Arg.Is<string>(json => json.Length > 0));
            results.Should().NotBeEmpty();
        }

        internal record FetchedQuote(string Anime, string Character, string Quote);

    }
}
