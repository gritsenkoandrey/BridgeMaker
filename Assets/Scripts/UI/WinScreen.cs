﻿using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class WinScreen : BaseScreen
    {
        [SerializeField] private Button _nextButton;

        protected override void Subscribe()
        {
            _nextButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    ScreenInterface.GetScreenInterface().Execute(ScreenType.LobbyScreen);

                    world.InstantiateLevel(true);
                })
                .AddTo(screenDisposable);
        }

        protected override void Unsubscribe()
        {
            screenDisposable.Clear();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}