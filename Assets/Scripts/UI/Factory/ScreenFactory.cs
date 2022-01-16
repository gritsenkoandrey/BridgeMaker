﻿using AssetPath;
using Managers;
using UI.Enum;
using UnityEngine;
using Utils;

namespace UI.Factory
{
    public sealed class ScreenFactory
    {
        private readonly Transform _canvas;

        private LobbyScreen _lobbyScreen;
        private GameScreen _gameScreen;
        private WinScreen _winScreen;
        private LoseScreen _loseScreen;

        public ScreenFactory()
        {
            _canvas = Manager.Resolve<MGUI>().GetCanvas.transform;
        }

        public LobbyScreen GetLobbyScreen()
        {
            if (!_lobbyScreen)
            {
                LobbyScreen screen = CustomResources.Load<LobbyScreen>(ScreenPath.Screens[ScreenType.LobbyScreen].path);
                _lobbyScreen = Object.Instantiate(screen, _canvas);
            }

            return _lobbyScreen;
        }

        public GameScreen GetGameScreen()
        {
            if (!_gameScreen)
            {
                GameScreen screen = CustomResources.Load<GameScreen>(ScreenPath.Screens[ScreenType.GameScreen].path);
                _gameScreen = Object.Instantiate(screen, _canvas);
            }

            return _gameScreen;
        }

        public WinScreen GetWinScreen()
        {
            if (!_winScreen)
            {
                WinScreen screen = CustomResources.Load<WinScreen>(ScreenPath.Screens[ScreenType.WinScreen].path);
                _winScreen = Object.Instantiate(screen, _canvas);
            }

            return _winScreen;
        }

        public LoseScreen GetLoseScreen()
        {
            if (!_loseScreen)
            {
                LoseScreen screen = CustomResources.Load<LoseScreen>(ScreenPath.Screens[ScreenType.LoseScreen].path);
                _loseScreen = Object.Instantiate(screen, _canvas);
            }

            return _loseScreen;
        }
    }
}