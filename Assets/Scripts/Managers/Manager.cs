using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public abstract class Manager : MonoBehaviour
    {
        private static readonly Dictionary<Type, Manager> Container = new Dictionary<Type, Manager>();

        private void OnEnable()
        {
            Register();
            
            Enable();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void Start()
        {
            Init();
        }

        protected abstract void Register();
        
        protected virtual void Init(){}
        protected virtual void Enable(){}
        protected virtual void Disable(){}

        protected static void RegisterManager<T>(T manager) where T : Manager
        {
            if (Container.ContainsKey(typeof(T)))
            {
                return;
            }
            
            Container.Add(typeof(T), manager);
        }

        protected static void UnregisterManager<T>(T manager) where T : Manager
        {
            Container.Remove(typeof(T));
        }

        public static T Resolve<T>() where T : Manager
        {
            if (!Container.ContainsKey(typeof(T)))
            {
                return null;
            }
            
            return Container[typeof(T)] as T;
        }
    }
}