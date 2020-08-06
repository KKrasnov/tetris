using System;
using Game.State;
using Tetris.Gameplay.Board;
using Tetris.Gameplay.Bricks.Components;
using Tetris.Gameplay.GameLoop;
using Tetris.InputManagement;
using Tools;
using UnityEngine;

namespace Tetris.Gameplay
{
    /// <summary>
    /// This might and must be separated into smaller chunks of functionality.
    /// </summary>
    public class GameManager : IObserver<InputReceivedParams>, IObserver<TickPassedParams>, IObserver<GameStateType>
    {
        private readonly IFactory<Brick> _brickFactory;
        private readonly IBoardManager _boardManager;
        private readonly IGameStateManager _gameStateManager;

        private Brick _currentBrick;
        private Transform[,] _blocks;

        public GameManager(IFactory<Brick> brickFactory, BoardManager boardManager, IGameStateManager gameStateManager)
        {
            _brickFactory = brickFactory;
            _boardManager = boardManager;
            _gameStateManager = gameStateManager;
        }

        public void OnNext(InputReceivedParams value)
        {
            if (value.Action == InputType.MoveLeft) Move(Vector2.left);
            else if (value.Action == InputType.MoveRight) Move(Vector2.right);
        }

        public void OnNext(TickPassedParams value)
        {
            ProcessTick();
        }

        public void OnNext(GameStateType value)
        {
            if (value == GameStateType.Game) StartNewSession();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        private void StartNewSession()
        {
            ClearSession();

            _boardManager.CreateNewBoard();
            _blocks = new Transform[_boardManager.Board.Width, _boardManager.Board.Height];
        }

        private void ClearSession()
        {
            foreach (var block in _blocks)
            {
                GameObject.Destroy(block.gameObject);
            }
        }

        private void ProcessTick()
        {
            if (_currentBrick == null)
            {
                _currentBrick = _brickFactory.GetNext();
                _currentBrick.SetPosition(_boardManager.Board.Width / 2, _boardManager.Board.Height);
            }
            else
            {
                if (CanMove(_currentBrick, Vector2.down))
                {
                    _currentBrick.DoMove(Vector2.down);
                }
                else
                {
                    PlaceBlocksOnBoard(_currentBrick);
                    _currentBrick.Deinit();
                    _currentBrick = null;
                    CheckForLineMatch();
                    CheckForGameEnd();
                }
            }
        }

        private void Move(Vector2 direction)
        {
            if (CanMove(_currentBrick, direction))
            {
                _currentBrick.DoMove(direction);
            }
        }

        private bool CanMove(Brick brick, Vector2 direction)
        {
            foreach (var block in _currentBrick.Blocks)
            {
                var resultPos = Vector2Int.RoundToInt(_currentBrick.GetPosition() + (Vector2)block.localPosition + direction);
                if (_boardManager.Board.GetCell(resultPos.x, resultPos.y).Status == Cell.StatusType.Occupied)
                {
                    return false;
                }
            }

            return true;
        }

        private void PlaceBlocksOnBoard(Brick brick)
        {
            for (int i = 0; i < _currentBrick.Blocks.Length; i++)
            {
                var block = _currentBrick.Blocks[i];
                var pos = Vector2Int.RoundToInt(_currentBrick.GetPosition() + (Vector2)block.localPosition);
                _blocks[pos.x, pos.y] = block;
                _boardManager.Board.SetCell(pos.x, pos.y, new Cell() {Status = Cell.StatusType.Occupied});
            }
        }

        private void CheckForLineMatch(int startLine = 0)
        {
            for (int y = startLine; y < _boardManager.Board.Height; y++)
            {
                bool isFilled = true;
                for (int x = 0; x < _boardManager.Board.Width; x++)
                {
                    if (_boardManager.Board.GetCell(x, y).Status != Cell.StatusType.Occupied)
                    {
                        isFilled = false;
                        break;
                    }
                }

                if (isFilled)
                {
                    RemoveLine(y);
                    CheckForLineMatch(y);
                    break;
                }
            }
        }

        private void RemoveLine(int line)
        {
            for (int x = 0; x < _boardManager.Board.Width; x++)
            {
                if (_boardManager.Board.GetCell(x, line).Status == Cell.StatusType.Occupied)
                {
                    GameObject.Destroy(_blocks[x, line].gameObject);
                }
            }
            
            for (int y = line; y < _boardManager.Board.Height; y++)
            {
                for (int x = 0; x < _boardManager.Board.Width; x++)
                {
                    Cell cellToReplace = y + 1 >= _boardManager.Board.Height ? 
                        new Cell(Cell.StatusType.Empty) : 
                        _boardManager.Board.GetCell(x, y + 1);
                    
                    if (cellToReplace.Status == Cell.StatusType.Occupied)
                    {
                        _blocks[x, y + 1].position += Vector3.down;
                    }

                    _boardManager.Board.SetCell(x, y, cellToReplace);
                }
            }
        }

        private void CheckForGameEnd()
        {
            int y = y = _boardManager.Board.Height -1;
            for (int x = 0; x < _boardManager.Board.Width; x++)
            {
                if (_boardManager.Board.GetCell(x, y).Status == Cell.StatusType.Occupied)
                {
                    _gameStateManager.SetState(GameStateType.MainMenu);
                }
            }
        }
    }
}