﻿using System.Collections.Generic;
using UI;
using UI.Enum;

namespace AssetPath
{
    public sealed class ScreenPath
    {
        public struct Screen
        {
            public string path;
            public Dictionary<ScreenType, string> elements;
        }

        public static readonly Dictionary<ScreenType, Screen> screens = new Dictionary<ScreenType, Screen>
        {
            {
                ScreenType.LobbyScreen, new Screen()
                {
                    path = "Prefabs/GUI/LobbyScreen",
                    elements = new Dictionary<ScreenType, string>
                    {
                        { ScreenType.LobbyScreen, "LobbyScreen"}
                    }
                }
            },
            {
                ScreenType.GameScreen, new Screen()
                {
                    path = "Prefabs/GUI/GameScreen",
                    elements = new Dictionary<ScreenType, string>
                    {
                        { ScreenType.GameScreen, "GameScreen"}
                    }
                }
            },
            {
                ScreenType.WinScreen, new Screen()
                {
                    path = "Prefabs/GUI/WinScreen",
                    elements = new Dictionary<ScreenType, string>
                    {
                        { ScreenType.WinScreen, "WinScreen"}
                    }
                }
            },
            {
                ScreenType.LoseScreen, new Screen()
                {
                    path = "Prefabs/GUI/LoseScreen",
                    elements = new Dictionary<ScreenType, string>
                    {
                        { ScreenType.LoseScreen, "LoseScreen"}
                    }
                }
            },
        };
    }
}