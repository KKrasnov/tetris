using Dependencies;
using UnityEngine;

namespace Bootstrap.Components
{
    public class Loader : MonoBehaviour
    {
        private void Awake()
        {
            InitializeApplication();
        }

        /// <summary>
        /// Main entry point.
        /// </summary>
        private void InitializeApplication()
        {
            var appConfig = new DependencyResolver();
            
            IDependencyBinder binder = GetComponent<IDependencyBinder>();//no time for fancy solution
            binder.ConfigureDependencies(appConfig);
            
            var appRunner = new AppRunner(appConfig);
            appRunner.ExecuteApplicationFlow();
        }
    }
}
