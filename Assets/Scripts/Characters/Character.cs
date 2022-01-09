using APP;
using BaseMonoBehaviour;
using Managers;
using UniRx;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class Character : BaseComponent
    {
        protected Animator animator;
        protected CharacterController characterController;

        protected MInput input;
        protected MWorld world;
        protected MGame game;

        protected readonly CompositeDisposable characterDisposable = new CompositeDisposable();

        protected override void Initialize()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            
            game.OnRoundEnd
                .Subscribe(_ =>
                {
                    characterDisposable.Clear();
                })
                .AddTo(this);
        }

        protected override void Enable()
        {
            input = APPCore.Instance.GetInput;
            world = APPCore.Instance.GetWorld;
            game = APPCore.Instance.GetGame;
        }

        protected override void Disable()
        {
            characterDisposable.Clear();
        }
    }
}