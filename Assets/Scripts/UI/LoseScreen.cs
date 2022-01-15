using DG.Tweening;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LoseScreen : BaseScreen
    {
        [SerializeField] private Button _restartButton;

        protected override void Subscribe()
        {
            base.Subscribe();
            
            _restartButton.transform.localScale = Vector3.zero;

            _restartButton.transform
                .DOScale(Vector3.one, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _restartButton
                        .OnClickAsObservable()
                        .First()
                        .Subscribe(_ =>
                        {
                            ScreenInterface.GetScreenInterface()
                                .Execute(ScreenType.LobbyScreen);

                            GetWorld.CreateLevel();
                        })
                        .AddTo(screenDisposable);
                });
        }

        protected override void Unsubscribe()
        {
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