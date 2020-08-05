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

        public class Data
        {
            public LevelData[] Levels;
            public Action<Guid> PlayAction;
        }
    }
}