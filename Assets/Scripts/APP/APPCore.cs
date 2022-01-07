using Managers;
using Patterns.Singleton;

namespace APP
{
    public sealed class APPCore : Singleton<APPCore>
    {
        public MInput input;
        public MGame game;
        public MWorld world;
        public MCamera mainCamera;
        public MLight directionLight;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}