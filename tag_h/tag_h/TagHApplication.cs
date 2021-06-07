using tag_h.Persistence;
using tag_h.Tasks;
using tag_h.Views;
using tag_h.Helper.Injection;

namespace tag_h
{
    [Injectable]
    public interface ITagHApplication
    {
        void Close();
        void Show();
    }

    public class TagHApplication : ITagHApplication
    {
        private readonly IMainWindow _mainWindow;
        private IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private ITaskRunner _taskRunner;

        public MainWindow MainWindow = null;

        public TagHApplication(
                IMainWindow mainWindow,
                IHImageRepository imageRepository, 
                ITagRepository tagRepository, 
                ITaskRunner taskRunner
            )
        {
            _mainWindow = mainWindow;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _taskRunner = taskRunner;

            _taskRunner.Submit(new SynchronisePersistence());
            _taskRunner.Submit(new IndexImages());
            _taskRunner.Submit(new DeleteDuplicates());
        }

        public void Show()
        {
            _mainWindow.UIStart();
        }

        public void Close()
        {
            _mainWindow.UIEnd();
            System.Windows.Application.Current.Shutdown();
        }

    }
}
