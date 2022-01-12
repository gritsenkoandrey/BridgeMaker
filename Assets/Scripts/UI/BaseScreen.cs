using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        protected MGUI gui;
        protected MGame game;
        protected MWorld world;
        
        protected Tweener tween;

        protected readonly CompositeDisposable screenDisposable = new CompositeDisposable();

        private void Awake()
        {
            gui = MContainer.Instance.GetGUI;
            game = MContainer.Instance.GetGame;
            world = MContainer.Instance.GetWorld;
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