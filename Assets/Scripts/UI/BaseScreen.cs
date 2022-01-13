using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        protected MGUI GetGUI { get; private set; }
        protected MGame GetGame { get; private set; }
        protected MWorld GetWorld { get; private set; }
        
        protected Tweener tween;

        protected readonly CompositeDisposable screenDisposable = new CompositeDisposable();

        private void Awake()
        {
            GetGUI = Manager.Resolve<MGUI>();
            GetGame = Manager.Resolve<MGame>();
            GetWorld = Manager.Resolve<MWorld>();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            screenDisposable.Clear();
            tween.KillTween();
        }

        protected virtual void Initialize() {}
        protected virtual void Subscribe() {}
        protected virtual void Unsubscribe() {}
        
        public abstract void Show();
        public abstract void Hide();
    }
}