using System;

namespace Game.State
{
    public class GameState
    {
        public readonly Action Enter;
        public readonly Action Exit;
        public GameState(Action enter, Action exit)
        {
            Enter = enter;
            Exit = exit;
        }
    }
}