using System;
using UI.Enum;
using UI.Factory;
using UniRx;

namespace Managers
{
    public sealed class MGame : Manager
    {
        public readonly ReactiveCommand OnRoundStart = new ReactiveCommand();
        public readonly ReactiveCommand<bool> OnRoundEnd = new ReactiveCommand<bool>();

        private readonly CompositeDisposable _gameDisposable = new CompositeDisposable();

        protected override void Register()
        {
            RegisterManager(this);
        }
        
        protected override void Disable()
        {
            base.Disable();
            
            UnregisterManager(this);
        }

        protected override void Init()
        {
            base.Init();
            
            OnRoundEnd
                .Subscribe(value =>
                {
                    Observable
                        .Timer(TimeSpan.FromSeconds(5f))
                        .Subscribe(_ =>
                        {
                            ScreenInterface.GetScreenInterface()
                                .Execute(value ? ScreenType.WinScreen : ScreenType.LoseScreen);
                        })
                        .AddTo(_gameDisposable);
                })
                .AddTo(this);
        }

        public override void Clear()
        {
            base.Clear();
            
            _gameDisposable.Clear();
        }
    }
}