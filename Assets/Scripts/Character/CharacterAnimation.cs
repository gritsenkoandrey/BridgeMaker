using APP;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private readonly CompositeDisposable _animatorDisposable = new CompositeDisposable();

        private MGame _game;

        private void OnEnable()
        {
            _game = APPCore.Instance.game;
        }

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _animator.SetFloat(Animations.Run, _characterController.velocity.magnitude, 0.05f, Time.deltaTime);
                })
                .AddTo(_animatorDisposable)
                .AddTo(this);

            _game.OnRoundEnd
                .Subscribe(_ =>
                {
                    _animatorDisposable.Clear();
                    _animator.SetTrigger(Animations.Victory);
                })
                .AddTo(this);
        }
    }
}