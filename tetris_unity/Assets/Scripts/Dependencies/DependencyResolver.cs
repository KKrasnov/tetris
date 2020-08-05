using System;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// Primitive, light-weight ioc-container/service-locator implementation.
    /// </summary>
    public class DependencyResolver
    {
        private Dictionary<Type, object> _dependenciesContainer = new Dictionary<Type, object>();
        
        public void Bind<T>(T dependency)
        {
            var type = typeof(T);
            if (_dependenciesContainer.ContainsKey(type))
            {
                throw new ArgumentException();
            }
            _dependenciesContainer.Add(type, dependency);
        }

        public T Resolve<T>()
        {
            return (T)_dependenciesContainer[typeof(T)];
        }
    }
}
