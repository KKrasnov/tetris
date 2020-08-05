using Dependencies;
using Game.State;

namespace Bootstrap
{
    public class AppRunner
    {
        private readonly DependencyResolver _dependencies;

        public AppRunner(DependencyResolver dependencies)
        {
            _dependencies = dependencies;
        }
        
        public void ExecuteApplicationFlow()
        {
            //it shouldn't be here actually
            _dependencies.Resolve<GameStateManager>().SetState(GameStateType.MainMenu);
        }
    }
}