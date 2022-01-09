using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(1)]
    public abstract class Manager : MonoBehaviour
    {
        public static readonly Dictionary<Type, Manager> Container = new Dictionary<Type, Manager>();

        private void Awake()
        {
            First();
        }

        private void OnEnable()
        {
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

        protected virtual void First(){}
        protected virtual void Init(){}
        protected virtual void Enable(){}
        protected virtual void Disable(){}
    }
}