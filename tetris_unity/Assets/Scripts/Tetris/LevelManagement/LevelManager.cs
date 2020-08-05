using System;

namespace Tetris.LevelManagement
{
    public class LevelManager
    {
        private readonly LevelData[] _levels;
        private readonly IObserver<LevelData> _levelChangedListener;

        private LevelData _currentLevel;

        public LevelData CurrentLevel
        {
            get => _currentLevel;
            set
            {
                if (_currentLevel != value)
                {
                    _currentLevel = value;
                    _levelChangedListener.OnNext(_currentLevel);
                }
            }
        }

        public LevelData[] Levels => _levels;

        public LevelManager(LevelData[] levels, IObserver<LevelData> levelChangedListener)
        {
            _levels = levels;
            _levelChangedListener = levelChangedListener;
        }
    }
}