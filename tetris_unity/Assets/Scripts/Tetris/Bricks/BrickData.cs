using System;
using UnityEngine;

namespace Tetris.Breaks
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