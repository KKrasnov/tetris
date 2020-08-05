using System;
using UnityEngine;//shouldn't be here

namespace Tetris.Bricks
{
    [Serializable]
    public class BrickData
    {
        public Vector2 Center;
        /// <summary>
        /// Should be some kind of CellData but whatever.
        /// </summary>
        public Vector2[] Cells;
    }
}