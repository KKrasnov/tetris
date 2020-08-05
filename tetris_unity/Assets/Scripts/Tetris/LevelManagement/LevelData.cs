using System;
using Tetris.Bricks;

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