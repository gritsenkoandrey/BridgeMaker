using BaseMonoBehaviour;
using Managers;
using UniRx;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class Character : BaseComponent
    {
        protected Animator Animator { get; private set; }
        protected CharacterController Controller { get; private set; }
        protected MInput GetInput { get; private set; }
        protected MWorld GetWorld { get; private set; }
        protected MGame GetGame { get; private set; }
        

        protected readonly CompositeDisposable characterDisposable = new CompositeDisposable();

        protected override void Init()
        {
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            
            GetGame.OnRoundEnd
                .Subscribe(_ => characterDisposable.Clear())
                .AddTo(this);
        }

        protected override void Enable()
        {
            base.Enable();
            
            GetInput = Manager.Resolve<MInput>();
            GetWorld = Manager.Resolve<MWorld>();
            GetGame = Manager.Resolve<MGame>();
        }

        protected override void Disable()
        {
            base.Disable();
            
            characterDisposable.Clear();
        }
    }
}