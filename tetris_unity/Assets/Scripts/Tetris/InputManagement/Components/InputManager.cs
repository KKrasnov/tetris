using System;
using UnityEngine;

namespace Tetris.InputManagement.Components
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        private IObserver<InputReceivedParams> _inputReceivedListener;

        public void Init(IObserver<InputReceivedParams> inputReceivedListener)
        {
            _inputReceivedListener = inputReceivedListener;
        }

        public void Send(InputType action)
        {
            _inputReceivedListener.OnNext(new InputReceivedParams() {Action = action});
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Send(InputType.MoveLeft);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Send(InputType.MoveRight);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Send(InputType.MoveDown);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Send(InputType.Rotate);
            }
        }
    }
}