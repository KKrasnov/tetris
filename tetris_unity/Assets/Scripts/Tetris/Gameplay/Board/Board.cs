using System;
using UnityEngine;

namespace Tetris.Gameplay.Board
{
    public class Board
    {
        private readonly Cell?[,] _grid;
        private readonly IObserver<BoardUpdatedParams> _boardUpdatedListener;

        public int Width => _grid.GetLength(0);
        public int Height => _grid.GetLength(1);

        public Board(Cell?[,] grid, IObserver<BoardUpdatedParams> boardUpdatedListener)
        {
            _grid = grid;
            _boardUpdatedListener = boardUpdatedListener;
        }

        public Cell GetCell(int x, int y)
        {
            if (_grid[x, y] == null)
            {
                _grid[x, y] = new Cell() {Status = Cell.StatusType.Empty};
            }

            return _grid[x, y].Value;
        }

        public Cell SetCell(int x, int y, Cell cell)
        {
            _grid[x, y] = cell;
            _boardUpdatedListener.OnNext(new BoardUpdatedParams()
            {
                Board = this,
                Position = new Vector2(x, y)
            });
            return cell;
        }
    }
}