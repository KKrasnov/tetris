using Game.State;
using Tetris.LevelManagement;

namespace Tetris.UI.MainMenu
{
    public class MainMenuWindowModel
    {
        private readonly IGameStateManager _gameStateManager;
        private readonly LevelManager _levelManager;

        public LevelData[] Levels => _levelManager.Levels;

        public LevelData CurrentLevel
        {
            get => _levelManager.CurrentLevel;
            set => _levelManager.CurrentLevel = value;
        }

        public GameStateType GameState
        {
            get => _gameStateManager.State;
            set => _gameStateManager.SetState(value);
        }

        public MainMenuWindowModel(IGameStateManager gameStateManager, LevelManager levelManager)
        {
            _gameStateManager = gameStateManager;
            _levelManager = levelManager;
        }
    }
}