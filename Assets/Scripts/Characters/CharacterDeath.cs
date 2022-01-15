using UniRx;
using UnityEngine;

namespace Characters
{
    public sealed class CharacterDeath : CharacterBase
    {
        protected override void Init()
        {
            base.Init();

            game.OnRoundEnd
                .Where(value  => !value)
                .Subscribe(value =>
                {
                    Debug.Log("Death FX");
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();
        }

        protected override void Disable()
        {
            base.Disable();
        }
    }
}