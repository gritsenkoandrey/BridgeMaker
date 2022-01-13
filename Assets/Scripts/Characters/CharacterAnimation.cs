using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterAnimation : Character
    {
        protected override void Init()
        {
            base.Init();
            
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Animator.SetFloat(Animations.Run, Controller.velocity.magnitude, 0.1f, Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(lifetimeDisposable);

            GetGame.OnRoundEnd
                .Subscribe(_ =>
                {
                    Animator.SetTrigger(Animations.Victory);
                })
                .AddTo(lifetimeDisposable);
        }
    }
}