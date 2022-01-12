using Patterns.Singleton;

namespace Managers
{
    public sealed class MContainer : Singleton<MContainer>
    {
        public MInput GetInput { get; private set; }
        public MGame GetGame { get; private set; }
        public MWorld GetWorld { get; private set; }
        public MCamera GetCamera { get; private set; }
        public MLight GetLight { get; private set; }
        public MGUI GetGUI { get; private set; }
        
        private void Awake()
        {
            InitManagers();
        }

        private void InitManagers()
        {
            GetInput = Manager.Resolve<MInput>();
            GetGame = Manager.Resolve<MGame>();
            GetWorld = Manager.Resolve<MWorld>();
            GetCamera = Manager.Resolve<MCamera>();
            GetLight = Manager.Resolve<MLight>();
            GetGUI = Manager.Resolve<MGUI>();
        }
    }
}