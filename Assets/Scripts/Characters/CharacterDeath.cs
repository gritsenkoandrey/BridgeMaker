using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterDeath : CharacterBase
    {
        [SerializeField] private Renderer _mesh;
        [SerializeField] private ParticleSystem[] _deathFX;
        
        protected override void Init()
        {
            base.Init();

            game.OnRoundEnd
                .Where(value  => !value)
                .Subscribe(value =>
                {
                    _mesh.enabled = false;
                    _deathFX.ForEach(fx => fx.Play());
                    world.CharacterItems.ForEach(i => i.onDrop.Execute(transform));
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