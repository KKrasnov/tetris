using Tetris.UI.Game.Components;
using UI.MVC;
using UI.MVC.Components;

namespace Tetris.UI.Game
{
    public class GameWindowController : IWindowController
    {
        private GameWindowModel _model;
        private GameWindowView _view;
        public BaseWindowView View => _view;
        public void Open(object model, BaseWindowView view)
        {
            _model = (GameWindowModel) model;
            _view = (GameWindowView) view;

            _view.Init((action)=> _model.RegisterInput(action));
        }

        public void Close()
        {
            
        }
    }
}