using BaseMonoBehaviour;
using Managers;
using UniRx;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterBase : BaseComponent
    {
        protected Animator animator;
        protected CharacterController characterController;
        
        protected MInput input;
        protected MWorld world;
        protected MGame game;
        protected MConfig config;
        
        protected readonly CompositeDisposable characterDisposable = new CompositeDisposable();

        protected override void Init()
        {
            base.Init();

            game
                .OnRoundEnd
                .Subscribe(_ =>
                {
                    characterDisposable.Clear();
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();

            input = Manager.Resolve<MInput>();
            world = Manager.Resolve<MWorld>();
            game = Manager.Resolve<MGame>();
            config = Manager.Resolve<MConfig>();
        }

        protected override void Disable()
        {
            base.Disable();
            
            characterDisposable.Clear();
        }
    }
}