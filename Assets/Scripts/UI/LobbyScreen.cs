using DG.Tweening;
using TMPro;
using UI.Tutorials;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Tutorial _tutorial;

        protected override void Initialize()
        {
            base.Initialize();
            
            tween = _text
                .DOScale(1.25f, 0.5f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            
            tween.Play();

            Init();

            _startButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    Game.OnRoundStart.Execute();
                })
                .AddTo(screenDisposable);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            
            tween.Pause();

            screenDisposable.Clear();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            
            _tutorial.Show();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
                        
            _tutorial.Hide();
        }

        private void Init()
        {
            _canvasGroup.alpha = 0f;
            
            _canvasGroup
                .DOFade(1f, 0f)
                .SetDelay(0.25f);
        }
    }
}