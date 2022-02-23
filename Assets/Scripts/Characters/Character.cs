using System.Collections.Generic;
using BaseMonoBehaviour;
using Managers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class Character : BaseComponent, ICharacter
    {
        private readonly List<ICharacter> _iCharacters = new List<ICharacter>();

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _root;
        [SerializeField] private GameObject _model;
        [SerializeField] private ParticleSystem[] _deathFX;

        private MGame _game;
        private MWorld _world;

        public void Construct()
        {
            _iCharacters.Add(new CharacterCollision(_characterController, _root));
            _iCharacters.Add(new CharacterAnimation(_characterController, _animator));
            _iCharacters.Add(new CharacterMovement(_characterController));
        }

        protected override void Init()
        {
            base.Init();
            
            Register();
            
            _world.ItemsColliders
                .ObserveRemove()
                .Where(_ => _world.ItemsColliders.Count == 0)
                .First()
                .Subscribe(_ =>
                {
                    CharacterVictory(true);
                })
                .AddTo(lifetimeDisposable);
            
            _characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Trap)
                .First()
                .Subscribe(col =>
                {
                    CharacterVictory(false);
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();

            _game = Manager.Resolve<MGame>();
            _world = Manager.Resolve<MWorld>();
        }

        protected override void Disable()
        {
            base.Disable();
            
            Unregistered();
                        
            _iCharacters.Clear();
        }

        public void Register()
        {
            _iCharacters.ForEach(c => c.Register());
        }

        public void Unregistered()
        {
            _iCharacters.ForEach(c => c.Unregistered());
        }

        private void CharacterVictory(bool isVictory)
        {
            Unregistered();
                    
            _game.OnRoundEnd.Execute(isVictory);

            if (isVictory)
            {
                _animator.SetTrigger(Animations.Victory);
            }
            else
            {
                _model.SetActive(false);
                        
                _deathFX.ForEach(fx => fx.Play());
                _world.CharacterItems.ForEach(i => i.onDrop.Execute(transform));
            }
        }
    }
}