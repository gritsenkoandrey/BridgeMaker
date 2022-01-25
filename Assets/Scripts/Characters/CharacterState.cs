using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterState : CharacterBase
    {
        [SerializeField] private Renderer _mesh;
        [SerializeField] private ParticleSystem[] _deathFX;
        
        protected override void Init()
        {
            base.Init();

            game.OnCharacterVictory
                .Where(value => !value)
                .First()
                .Subscribe(_ =>
                {
                    _mesh.enabled = false;
                    _deathFX.ForEach(fx => fx.Play());
                    
                    world.CharacterItems.ForEach(i => i.onDrop.Execute(transform));

                    world.CharacterItems.Clear();
                    game.OnRoundEnd.Execute(false);
                })
                .AddTo(lifetimeDisposable);
            
            world.ItemsColliders
                .ObserveRemove()
                .Where(_ => world.ItemsColliders.Count == 0)
                .First()
                .Subscribe(_ =>
                {
                    gameObject.layer = Layers.Deactivate;
                    
                    game.OnCharacterVictory.Execute(true);
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