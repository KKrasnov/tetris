using System;
using Tetris.Gameplay.Bricks;

namespace Tetris.LevelManagement
{
    [Serializable]
    public class LevelData
    {
        public Guid ID;
        public float TickDuration;
        public BrickData[] BricksPresets;
    }
}