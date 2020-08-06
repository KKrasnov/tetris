using Tetris.InputManagement;

namespace Tetris.UI.Game
{
    public class GameWindowModel
    {
        private IInputManager _inputManager;

        public void RegisterInput(InputType action)
        {
            _inputManager.Send(action);
        }
    }
}