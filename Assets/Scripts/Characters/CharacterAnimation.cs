using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterAnimation : ICharacter
    {
        private readonly CharacterController _controller;
        private readonly Animator _animator;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterAnimation(CharacterController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }

        public void Register()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _animator.SetFloat(Animations.Run,
                        _controller.velocity.magnitude, 0.1f, Time.deltaTime);
                })
                .AddTo(_disposable);
        }

        public void Unregistered()
        {
            _disposable.Clear();
        }
    }
}