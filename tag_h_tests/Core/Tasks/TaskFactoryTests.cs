using NUnit.Framework;
using NSubstitute;
using FluentAssertions;

using Serilog;

using tag_h.Core.Tasks;
using tag_h.Injection.DI;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h_tests.Core.Tasks
{
    class TaskFactoryTests
    {

        private ITaskFactory _sut;

        private IServiceLocator _serviceLocator;

        [SetUp]
        public void Setup()
        {
            _serviceLocator = Substitute.For<IServiceLocator>();
            _sut = new TaskFactory(Substitute.For<ILogger>(), _serviceLocator);
        }

        [Test]
        public void CreateTask_WithTestTask_GetsCreated()
        {
            var testService = new TestService();
            _serviceLocator.Resolve(typeof(ITestService)).Returns(testService);

            var testTask =_sut.CreateTask<TestTask>();

            testTask.TaskName.Should().Be(nameof(TestTask));
            testTask.TestService.Should().Be(testService);
        }

        internal interface ITestService { int Value { get; } }

        internal class TestService : ITestService { public int Value => 10; }

        internal class TestTask : ITask
        {
            public string TaskName => nameof(TestTask);

            public ITestService TestService { get; }

            public TestTask(ITestService testService) => TestService = testService;

            public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
            {
                throw new System.NotImplementedException();
            }
        }

    }
}
