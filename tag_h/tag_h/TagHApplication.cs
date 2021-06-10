using tag_h.Tasks;
using tag_h.Views;
using tag_h.Injection;

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
        private ITaskRunner _taskRunner;

        public MainWindow MainWindow = null;

        public TagHApplication(
                IMainWindow mainWindow,
                ITaskRunner taskRunner
            )
        {
            _mainWindow = mainWindow;
            _mainWindow.SetApplication(this);

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
