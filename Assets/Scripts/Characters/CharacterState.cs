using System;
using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterState : CharacterBase
    {
        [SerializeField] private Renderer _mesh;
        [SerializeField] private ParticleSystem[] _deathFX;
        [SerializeField] private ParticleSystem _winFX;

        private readonly CompositeDisposable _timerDisposable = new CompositeDisposable();
        
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

                    int count = 0;
                    
                    Observable
                        .Interval(TimeSpan.FromSeconds(1f))
                        .Subscribe(_ =>
                        {
                            count++;
                            _winFX.Play();

                            if (count == 3)
                            {
                                _timerDisposable.Clear();
                            }
                        })
                        .AddTo(_timerDisposable)
                        .AddTo(lifetimeDisposable);
                    
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