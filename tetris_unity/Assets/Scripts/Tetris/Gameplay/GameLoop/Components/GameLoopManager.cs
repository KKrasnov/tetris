using System;
using Game.State;
using Tetris.InputManagement;
using Tetris.LevelManagement;
using UnityEngine;

namespace Tetris.Gameplay.GameLoop.Components
{
    public class GameLoopManager : MonoBehaviour, IObserver<LevelData>, IObserver<GameStateType>, IObserver<InputReceivedParams>
    {
        private IObserver<TickPassedParams> _tickPassedListener;
        
        private float _tickDuration;
        private bool _isRunning = false;

        private float _tickDelta = 0;
        
        public void Init(IObserver<TickPassedParams> tickPassedListener)
        {
            _tickPassedListener = tickPassedListener;
        }

        public void OnNext(GameStateType value)
        {
            _isRunning = value == GameStateType.Game;
            _tickDelta = _isRunning ? _tickDelta : 0;
        }

        public void OnNext(LevelData value)
        {
            _tickDuration = value.TickDuration;
        }

        public void OnNext(InputReceivedParams value)
        {
            if (value.Action == InputType.MoveDown)
            {
                TickPassed();
            }
        }
        
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            if(!_isRunning) return;

            _tickDelta += Mathf.Clamp01(Time.deltaTime / _tickDuration);

            if (_tickDelta >= 1) TickPassed();
        }

        private void TickPassed()
        {
            _tickPassedListener.OnNext(new TickPassedParams());
            _tickDelta = 0;
        }
    }
}