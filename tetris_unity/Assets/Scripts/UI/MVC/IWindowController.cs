using UI.MVC.Components;

namespace UI.MVC
{
    public interface IWindowController
    {
        BaseWindowView View { get; }
        void Open(object model, BaseWindowView view);
        /// <summary>
        /// Consider as `Dispose`
        /// </summary>
        void Close();
    }
}