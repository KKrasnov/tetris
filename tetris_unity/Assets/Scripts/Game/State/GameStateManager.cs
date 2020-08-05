using System;
using System.Linq;

namespace Game.State
{
    public class GameStateManager
    {
        private readonly IObserver<GameStateType> _stateChangeListener;
        private readonly Tuple<GameStateType, Func<GameState>>[] _stateFactory;

        private GameState _state;
        private GameStateType? _stateType;

        public GameStateType State => _stateType.Value;

        public GameStateManager(Tuple<GameStateType, Func<GameState>>[] stateFactory,
            IObserver<GameStateType> stateChangeListener)
        {
            _stateFactory = stateFactory;
            _stateChangeListener = stateChangeListener;
        }

        public void SetState(GameStateType state)
        {
            if (_stateType != state)
            {
                _state?.Exit();
                _state = _stateFactory.First(fac => fac.Item1 == state).Item2();
                _stateType = state;
                _state.Enter();
                _stateChangeListener.OnNext(state);
            }
        }
    }
}