using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services
{
    internal class ServiceProvider : MonoBehaviour
    {
        [SerializeField] private List<GameObject> servs = new();
        private Dictionary<Type, IService> services = new();

        private void Awake()
        {
            servs.ForEach(service => RegisterService(service.GetComponent<IService>()));
        }

        public T GetService<T> () where T : IService 
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out IService service)) return (T)service;
            throw new NotSupportedException();
        }
        public IEnumerator Initialize()
        {
            foreach (var service in services)
                yield return service.Value.Initialize();
        }

        private void RegisterService(IService service)
        {
            if (service == null) throw new NullReferenceException();
            Type type = service.GetType();
            services.Add(type, service);
        }

    }
}
