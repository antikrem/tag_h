using System.Threading.Tasks;

using NUnit.Framework;
using FluentAssertions;
using NSubstitute;

using EphemeralEx.Serialisation;
using EphemeralEx.Tests;

using tag_h.Core.TagRetriever.TagSource;
using tag_h.Core.TagRetriever;
using tag_h.Core.Persistence;
using tag_h.Core.Model;


namespace tag_h_tests.Core.TagRetriever.TagSource
{
    class CureNinjaBooruTagSourceTests
    {
        private HImage _image;
        private IImageHasher _imageHasher;
        private CureNinjaBooruTagSource _sut;

        [SetUp]
        public void Setup()
        {
            _image = new HImage(Dummy.Int(), Dummy.String());

            IFetchHandler fetchHandler = new FetchHandler(new Jsonifier());
            _imageHasher = Substitute.For<IImageHasher>();

            _sut = new CureNinjaBooruTagSource(Substitute.For<ITagMaterialiser>(), fetchHandler, _imageHasher);
        }

        [Test]
        public async Task RetrieveTags_WithValidHash_ReturnTags()
        {
            _imageHasher.GetHash(_image).Returns(new ImageHash("d3352244080ab8c04980bb1fba5848aa", null));

            var tags = await _sut.RetrieveTags(_image);

            tags.Should().NotBeEmpty();
        }

        [Test]
        public async Task RetrieveTags_WithInvalidHash_ReturnTags()
        {
            _imageHasher.GetHash(_image).Returns(new ImageHash("invalidhash", null));

            var tags = await _sut.RetrieveTags(_image);

            tags.Should().BeEmpty();
        }
    }
}
