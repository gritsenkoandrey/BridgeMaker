using APP;
using DG.Tweening;
using Managers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public sealed class GameScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _endButton;
        [SerializeField] private Button _restartButton;

        [SerializeField] private CanvasGroup _startGame;
        [SerializeField] private CanvasGroup _endGame;
        [SerializeField] private CanvasGroup _fade;

        private MGame _game;
        private MWorld _world;

        private Tweener _tween;

        private void OnEnable()
        {
            _game = APPCore.Instance.game;
            _world = APPCore.Instance.world;
            
            _startButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _game.OnRoundStart.Execute();

                    SetActiveStartElement(false, 0f);
                    
                    _tween.Pause();
                })
                .AddTo(this);

            _endButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    SetActiveCanvasGroup(_startGame, true, 1f);
                    SetActiveCanvasGroup(_endGame, false, 0f);

                    SetActiveStartElement(true, 1f);

                    GUIFade();

                    _tween.Play();

                    _world.InstantiateLevel(true);
                })
                .AddTo(this);

            _restartButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _restartButton.transform
                        .DOScale(Vector3.one * 0.5f, 0.5f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() =>
                        {
                            SetActiveStartElement(true, 1f);
                            
                            GUIFade();

                            _tween.Play();

                            _world.InstantiateLevel();

                            _restartButton.transform
                                .DOScale(Vector3.one, 0.5f)
                                .SetEase(Ease.OutBack);
                        });
                })
                .AddTo(this);

            _game.OnRoundEnd
                .Subscribe(_ =>
                {
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        SetActiveCanvasGroup(_startGame, false, 0f);
                        SetActiveCanvasGroup(_endGame, true, 1f);
                    });
                })
                .AddTo(this);
        }

        private void OnDisable()
        {
            _tween.KillTween();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            SetActiveCanvasGroup(_startGame, true, 1f);
            SetActiveCanvasGroup(_endGame, false, 0f);

            _fade.alpha = 0f;

            _tween = _text
                .DOScale(1.25f, 0.5f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void GUIFade()
        {
            _fade.alpha = 1f;
            
            _fade
                .DOFade(0f, 1f)
                .SetEase(Ease.Linear);
        }

        private void SetActiveStartElement(bool value, float alpha)
        {
            _startButton.gameObject.SetActive(value);
            _text.DOFade(alpha, 0.25f).SetEase(Ease.Linear);
        }

        private static void SetActiveCanvasGroup(CanvasGroup canvasGroup, bool value, float alpha)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}