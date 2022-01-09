using DG.Tweening;
using TMPro;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public sealed class GameScreen : BaseScreen
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _countItemsText;

        protected override void Subscribe()
        {
            int max = world.ItemsColliders.Count;
            int cur = 0;

            _countItemsText.text = $"{cur}/{max}";
            _countItemsText.transform.localScale = Vector3.zero;
            _countItemsText.transform
                .DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);
            
            _restartButton.transform.localScale = Vector3.zero;
            _restartButton.transform
                .DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);
            
            _restartButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    _restartButton.transform
                        .DOScale(Vector3.one * 0.5f, 0.5f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() =>
                        {
                            world.InstantiateLevel();
                            
                            ScreenInterface.GetScreenInterface()
                                .Execute(ScreenType.LobbyScreen);

                            _restartButton.transform
                                .DOScale(Vector3.one, 0.5f)
                                .SetEase(Ease.OutBack);
                        });
                })
                .AddTo(screenDisposable);

            world.ItemsColliders
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    cur++;
                    
                    _countItemsText.text = $"{cur}/{max}";
                    
                    tween.KillTween();

                    tween = _countItemsText
                        .DOScale(1.25f, 0.2f)
                        .SetEase(Ease.Linear)
                        .SetLoops(2, LoopType.Yoyo);
                })
                .AddTo(screenDisposable);
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