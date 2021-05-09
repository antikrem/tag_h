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
        private  IImageDatabase _imageDataBase;
        private ITaskRunner _taskRunner;

        private MainWindow _mainWindow;

        public MainWindow MainWindow = null;

        public TagHApplication(IImageDatabase imageDatabase, ITaskRunner taskRunner)
        {
            _imageDataBase = imageDatabase;

            _taskRunner = taskRunner;

            _taskRunner.Submit(new SynchronisePersistence());
            _taskRunner.Submit(new DeleteDuplicates());
        }

        public void Show()
        {
            _mainWindow = new MainWindow(_imageDataBase, this);
            _mainWindow.ShowDialog();
        }

        public void Close()
        {
            _mainWindow.Close();
            System.Windows.Application.Current.Shutdown();
        }

    }
}
