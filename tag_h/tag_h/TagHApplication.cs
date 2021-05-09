using System;

using tag_h.Persistence;
using tag_h.Tasks;
using tag_h.Model;
using tag_h.Views;

namespace tag_h
{
    public interface ITagHApplication
    {
        void Close();
    }

    public class TagHApplication : ITagHApplication
    {
        public IImageDatabase ImageDataBase { get; } = null;
        private ITaskRunner _taskRunner;

        public MainWindow MainWindow = null;

        public TagHApplication()
        {
            this.ImageDataBase = new ImageDatabase();

            _taskRunner = new TaskRunner(ImageDataBase);

            _taskRunner.Submit(new SynchronisePersistence());
            _taskRunner.Submit(new DeleteDuplicates());

            var window = new MainWindow(this.ImageDataBase, this);
            window.Show();
        }

        public void Close()
        {
            End();
            System.Windows.Application.Current.Shutdown();
        }

        private void End()
        {
            _taskRunner.Stop();
            this.ImageDataBase.Dispose();
        }
    }
}
