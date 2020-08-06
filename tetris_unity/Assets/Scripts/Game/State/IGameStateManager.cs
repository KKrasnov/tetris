namespace Game.State
{
    public interface IGameStateManager
    {
        GameStateType State { get; }
        void SetState(GameStateType state);
    }
}