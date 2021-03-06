using System;

namespace Tetris.Gameplay.Board
{
    public class BoardManager : IBoardManager, IObserver<BoardUpdatedParams>
    {
        public Board Board { get; set; }

        public void CreateNewBoard()
        {
            Board = new Board(new Cell?[10,15], this);
        }

        public void OnNext(BoardUpdatedParams value)
        {
            //do nothing
        }
        
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}