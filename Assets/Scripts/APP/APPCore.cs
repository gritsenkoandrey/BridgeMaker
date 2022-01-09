using Managers;
using Patterns.Singleton;

namespace APP
{
    public sealed class APPCore : Singleton<APPCore>
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

            DontDestroyOnLoad(this);
        }

        private void InitManagers()
        {
            GetInput = Manager.Container[typeof(MInput)].GetComponent<MInput>();
            GetGame = Manager.Container[typeof(MGame)].GetComponent<MGame>();
            GetWorld = Manager.Container[typeof(MWorld)].GetComponent<MWorld>();
            GetCamera = Manager.Container[typeof(MCamera)].GetComponent<MCamera>();
            GetLight = Manager.Container[typeof(MLight)].GetComponent<MLight>();
            GetGUI = Manager.Container[typeof(MGUI)].GetComponent<MGUI>();
        }
    }
}