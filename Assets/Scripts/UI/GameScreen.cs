using System.Linq;
using DG.Tweening;
using TMPro;
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

        private int _max;
        private int _cur;

        protected override void Subscribe()
        {
            base.Subscribe();
            
            Init();

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
                            Game.LaunchRound.Execute(false);

                            _restartButton.transform
                                .DOScale(Vector3.one, 0.5f)
                                .SetEase(Ease.OutBack);
                        });
                })
                .AddTo(screenDisposable);

            World.CharacterItems
                .ObserveAdd()
                .Subscribe(_ =>
                {
                    RefreshCountItem();
                })
                .AddTo(screenDisposable);
            
            World.CharacterItems
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    RefreshCountItem();
                })
                .AddTo(screenDisposable);

            World.CharacterItems
                .ObserveReset()
                .Subscribe(_ =>
                {
                    RefreshCountItem();
                })
                .AddTo(screenDisposable);

            World.Platforms
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    _max = GetMaxItemOnCurrentPlatform();
                    
                    RefreshCountItem();
                })
                .AddTo(screenDisposable);

            World.ItemsColliders
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    tween.KillTween();

                    tween = _countItemsText
                        .DOScale(1.25f, 0.2f)
                        .SetEase(Ease.InSine)
                        .SetLoops(2, LoopType.Yoyo);
                })
                .AddTo(screenDisposable);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            
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

        private void Init()
        {
            _max = GetMaxItemOnCurrentPlatform();
            _cur = 0;

            _countItemsText.text = $"{_cur}/{_max}";

            _countItemsText.transform.localScale = Vector3.zero;
            
            _countItemsText.transform
                .DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);

            _restartButton.transform.localScale = Vector3.zero;
            
            _restartButton.transform
                .DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);
        }

        private int GetMaxItemOnCurrentPlatform()
        {
            return World.Platforms.OrderBy(p => p.Index).First().Count;
        }

        private void RefreshCountItem()
        {
            _countItemsText.text = $"{World.CharacterItems.Count}/{_max}";
        }
    }
}