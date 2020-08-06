namespace Tetris.Gameplay.Board
{
    public struct Cell
    {
        public enum StatusType
        {
            Empty,
            Occupied
        }

        public StatusType Status;

        public Cell(StatusType status)
        {
            Status = status;
        }
    }
}