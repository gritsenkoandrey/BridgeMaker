using System;
using DG.Tweening;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(100)]
    public sealed class MGame : Manager
    {
        private MConfig _config;
        private MGUI _gui;
        private MWorld _world;
        private MInput _input;
        
        public readonly ReactiveCommand OnRoundStart = new ReactiveCommand();
        public readonly ReactiveCommand<bool> OnRoundEnd = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> LounchRound = new ReactiveCommand<bool>();

        public readonly ReactiveCommand<bool> OnCharacterVictory = new ReactiveCommand<bool>();

        private readonly CompositeDisposable _gameDisposable = new CompositeDisposable();

        protected override void Register()
        {
            RegisterManager(this);
        }

        protected override void Enable()
        {
            base.Enable();
                        
            _config = Resolve<MConfig>();
            _gui = Resolve<MGUI>();
            _world = Resolve<MWorld>();
            _input = Resolve<MInput>();
            
            OnRoundStart
                .Subscribe(_ =>
                {
                    ScreenInterface.GetScreenInterface().Execute(ScreenType.GameScreen);
                    
                    _input.IsEnable.SetValueAndForceNotify(true);
                })
                .AddTo(managerDisposable);
            
            OnRoundEnd
                .Subscribe(value =>
                {
                    float time = value ? _config.SettingsData.GetTimeToWin : _config.SettingsData.GetTimeToLose;

                    Observable
                        .Timer(TimeSpan.FromSeconds(time))
                        .Subscribe(_ =>
                        {
                            ScreenInterface.GetScreenInterface()
                                .Execute(value ? ScreenType.WinScreen : ScreenType.LoseScreen);
                        })
                        .AddTo(_gameDisposable);
                    
                    _input.IsEnable.SetValueAndForceNotify(false);
                })
                .AddTo(managerDisposable);

            LounchRound
                .Subscribe(value =>
                {
                    ScreenInterface.GetScreenInterface().Execute(ScreenType.LobbyScreen);

                    _gui.GetFade.DOFade(1f, 0f);
                    
                    _gui.GetFade.DOFade(0f, 0.1f)
                        .SetEase(Ease.Linear)
                        .SetDelay(0.25f);
                    
                    Clear();
                    
                    _world.LoadLevel(value);
                })
                .AddTo(managerDisposable);
        }

        protected override void Disable()
        {
            base.Disable();
            
            UnregisterManager(this);
        }

        protected override void Init()
        {
            base.Init();

            LounchRound.Execute(false);
        }

        private void Clear()
        {
            _gameDisposable.Clear();
        }
    }
}