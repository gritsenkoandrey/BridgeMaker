using DG.Tweening;
using UI.Enum;
using UI.Factory;
using UniRx;

namespace Managers
{
    public sealed class MGame : Manager
    {
        public readonly ReactiveCommand OnRoundStart = new ReactiveCommand();
        public readonly ReactiveCommand<bool> OnRoundEnd = new ReactiveCommand<bool>();

        protected override void Register()
        {
            RegisterManager(this);
        }
        
        protected override void Disable()
        {
            UnregisterManager(this);
        }

        protected override void Init()
        {
            OnRoundEnd
                .Subscribe(value =>
                {
                    DOVirtual.DelayedCall(5f, () =>
                    {
                        ScreenInterface.GetScreenInterface()
                            .Execute(value ? ScreenType.WinScreen : ScreenType.LoseScreen);
                    });
                })
                .AddTo(this);
        }
    }
}