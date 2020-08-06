using System;
using UnityEngine;

namespace Tetris.InputManagement
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        private IObserver<InputReceivedParams> _inputReceivedListener;

        public void SetupListener(IObserver<InputReceivedParams> inputReceivedListener)
        {
            _inputReceivedListener = inputReceivedListener;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _inputReceivedListener.OnNext(new InputReceivedParams(){Action = InputType.MoveLeft});
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _inputReceivedListener.OnNext(new InputReceivedParams(){Action = InputType.MoveRight});
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _inputReceivedListener.OnNext(new InputReceivedParams(){Action = InputType.MoveDown});
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _inputReceivedListener.OnNext(new InputReceivedParams(){Action = InputType.Rotate});
            }
        }

        public void Send(InputType action)
        {
            _inputReceivedListener.OnNext(new InputReceivedParams(){Action = action});
        }
    }
}