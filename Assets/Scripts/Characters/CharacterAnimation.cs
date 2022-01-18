using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public sealed class CharacterAnimation : CharacterBase
    {
        protected override void Enable()
        {
            base.Enable();

            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        protected override void Init()
        {
            base.Init();
            
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    animator.SetFloat(Animations.Run, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(lifetimeDisposable);
        }
    }
}