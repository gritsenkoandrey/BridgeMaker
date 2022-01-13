using DG.Tweening;
using TMPro;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _text;

        protected override void Initialize()
        {
            tween = _text
                .DOScale(1.25f, 0.5f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        protected override void Subscribe()
        {
            tween.Play();

            _text.DOFade(0f, 0f);
            _text.DOFade(1f, 0f).SetDelay(0.25f);
            
            GetGUI.GetImage.DOFade(1f, 0f);
            GetGUI.GetImage.DOFade(0f, 0f).SetDelay(0.25f);

            _startButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    GetGame.OnRoundStart.Execute();
                    
                    ScreenInterface.GetScreenInterface()
                        .Execute(ScreenType.GameScreen);
                })
                .AddTo(screenDisposable);
        }

        protected override void Unsubscribe()
        {
            tween.Pause();

            screenDisposable.Clear();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}