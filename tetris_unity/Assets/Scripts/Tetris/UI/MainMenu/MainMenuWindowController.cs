using System;
using System.Linq;
using Game.State;
using Tetris.UI.MainMenu.Components;
using UI.MVC;
using UI.MVC.Components;

namespace Tetris.UI.MainMenu
{
    public class MainMenuWindowController : IWindowController
    {
        private MainMenuWindowModel _model;
        private MainMenuWindowView _view;

        public BaseWindowView View => _view;

        public void Open(object model, BaseWindowView view)
        {
            _model = (MainMenuWindowModel)model;
            _view = (MainMenuWindowView)view;
            
            _view.Init(new MainMenuWindowView.Data()
            {
                Levels = _model.Levels,
                PlayAction = PlayLevel
            });
        }

        public void Close()
        {
        }

        private void PlayLevel(Guid id)
        {
            _model.CurrentLevel = _model.Levels.First(lvl => lvl.ID == id);
            _model.GameState = GameStateType.Game;
        }
    }
}