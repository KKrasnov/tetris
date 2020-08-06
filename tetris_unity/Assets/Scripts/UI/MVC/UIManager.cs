using System;
using System.Collections.Generic;
using UI.MVC.Components;
using UnityEngine;//shouldn't be here

namespace UI.MVC
{
    /// <summary>
    /// This manager is coupled to UnityEngine since I didn't want
    /// to bother separating views instantiation functionality. However, not an impossible task to do.
    /// </summary>
    public class UIManager : IUIManager
    {
        private readonly Dictionary<string, UIWindowConfiguration> _windowsMap;//ahh no time to perform proper mapping

        private readonly Canvas _windowsContainer;
        
        private Dictionary<string, IWindowController> _activeWindows = new Dictionary<string, IWindowController>();
        
        public UIManager(Canvas windowsContainer, Dictionary<string, UIWindowConfiguration> windowsMap)
        {
            _windowsMap = windowsMap;
            _windowsContainer = windowsContainer;
        }
        
        public void OpenWindow(string id)//I prefer to stick to string identifiers instead of enums but I have no strong opinion on it yet.
        {
            var configuration = _windowsMap[id];
            var controller = configuration.ControllerFactory();
            var model = configuration.ModelFactory();
            var view = CreateWindowView(configuration.ViewPrefab);
            
            controller.Open(model, view);
            
            _activeWindows.Add(id, controller);
        }

        public void CloseWindow(string id)
        {
            var controller = _activeWindows[id];
            controller.Close();
            controller.View.Close();
            DestroyWindowView(controller.View);
            _activeWindows.Remove(id);
        }
        
        private BaseWindowView CreateWindowView(GameObject prefab)
        {
            GameObject inst = GameObject.Instantiate(prefab);
            inst.transform.SetParent(_windowsContainer.transform);
            inst.transform.localPosition = Vector3.zero;
            inst.transform.localScale = Vector3.one;
            return inst.GetComponent<BaseWindowView>();
        }

        private void DestroyWindowView(BaseWindowView view)
        {
            GameObject.Destroy(view.gameObject);
        }
    }

    public class UIWindowConfiguration
    {
        public Func<IWindowController> ControllerFactory;
        public Func<object> ModelFactory;
        public GameObject ViewPrefab;

        public UIWindowConfiguration(Func<IWindowController> controllerFactory, Func<object> modelFactory, GameObject viewPrefab)
        {
            ControllerFactory = controllerFactory;
            ModelFactory = modelFactory;
            ViewPrefab = viewPrefab;
        }
    }
}