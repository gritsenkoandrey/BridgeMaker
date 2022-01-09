using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterAnimation : Character
    {
        protected override void Initialize()
        {
            base.Initialize();
            
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    animator.SetFloat(Animations.Run, characterController.velocity.magnitude, 0.05f, Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(this);

            game.OnRoundEnd
                .Subscribe(_ =>
                {
                    animator.SetTrigger(Animations.Victory);
                })
                .AddTo(this);
        }
    }
}