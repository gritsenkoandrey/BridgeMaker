using DG.Tweening;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class WinScreen : BaseScreen
    {
        [SerializeField] private Button _nextButton;

        protected override void Subscribe()
        {
            _nextButton.transform.localScale = Vector3.zero;

            _nextButton.transform
                .DOScale(Vector3.one, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _nextButton
                        .OnClickAsObservable()
                        .First()
                        .Subscribe(_ =>
                        {
                            ScreenInterface.GetScreenInterface()
                                .Execute(ScreenType.LobbyScreen);

                            world.InstantiateLevel(true);
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