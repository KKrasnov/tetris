using System;
using Tetris.InputManagement;
using UI.MVC.Components;

namespace Tetris.UI.Game.Components
{
    public class GameWindowView : BaseWindowView
    {
        private Action<InputType> _inputRegisteredCallback;
        public void Init(Action<InputType> inputRegisteredCallback)
        {
            _inputRegisteredCallback = inputRegisteredCallback;
        }

        public void LeftPressed()
        {
            _inputRegisteredCallback?.Invoke(InputType.MoveLeft);
        }

        public void RightPressed()
        {
            _inputRegisteredCallback?.Invoke(InputType.MoveRight);
        }

        public void DownPressed()
        {
            _inputRegisteredCallback?.Invoke(InputType.MoveDown);
        }

        public void RotatePressed()
        {
            _inputRegisteredCallback?.Invoke(InputType.Rotate);
        }
    }
}