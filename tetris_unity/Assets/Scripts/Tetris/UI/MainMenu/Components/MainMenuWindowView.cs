using System;
using Tetris.LevelManagement;
using UI.MVC.Components;

namespace Tetris.UI.MainMenu.Components
{
    public class MainMenuWindowView : BaseWindowView
    {
        private Data _data;
        
        public void Init(Data data)
        {
            _data = data;
        }

        public void PlayPressed()
        {
            _data.PlayAction(_data.Levels[0].ID);
        }

        public class Data
        {
            public LevelData[] Levels;
            public Action<Guid> PlayAction;
        }
    }
}