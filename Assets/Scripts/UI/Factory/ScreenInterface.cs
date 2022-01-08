using UI.Enum;

namespace UI.Factory
{
    public sealed class ScreenInterface
    {
        private BaseScreen _currentScreen;
        private readonly ScreenFactory _screenFactory;
        private static ScreenInterface _screenInterface;

        private ScreenInterface() => _screenFactory = new ScreenFactory();

        public static ScreenInterface GetScreenInterface()
        {
            if (_screenInterface != null)
            {
                return _screenInterface;
            }

            return _screenInterface = new ScreenInterface();
        }

        public void Execute(ScreenType screenType)
        {
            if (_currentScreen)
            {
                _currentScreen.Hide();
            }

            _currentScreen = screenType switch
            {
                ScreenType.LobbyScreen => _screenFactory.GetLobbyScreen(),
                ScreenType.GameScreen => _screenFactory.GetGameScreen(),
                ScreenType.WinScreen => _screenFactory.GetWinScreen(),
                ScreenType.LoseScreen => _screenFactory.GetLoseScreen(),
                _ => _currentScreen
            };

            if (_currentScreen)
            {
                _currentScreen.Show();
            }
        }

        public static void Clean()
        {
            _screenInterface = null;
        }
    }
}