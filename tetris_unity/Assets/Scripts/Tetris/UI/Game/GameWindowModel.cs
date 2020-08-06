using Tetris.InputManagement;

namespace Tetris.UI.Game
{
    public class GameWindowModel
    {
        private IInputManager _inputManager;

        public GameWindowModel(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public void RegisterInput(InputType action)
        {
            _inputManager.Send(action);
        }
    }
}