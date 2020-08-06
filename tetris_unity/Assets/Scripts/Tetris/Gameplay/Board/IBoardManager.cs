namespace Tetris.Gameplay.Board
{
    public interface IBoardManager
    {
        Board Board { get; }
        void CreateNewBoard();
    }
}