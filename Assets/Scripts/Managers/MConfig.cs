using AssetPath;
using Data;
using Utils;

namespace Managers
{
    public sealed class MConfig : Manager
    {
        public CharacterData CharacterData { get; private set; }
        public EnvironmentData EnvironmentData { get; private set; }
        
        protected override void Register()
        {
            RegisterManager(this);
        }

        protected override void Init()
        {
            base.Init();

            CharacterData = CustomResources.Load<CharacterData>(DataPath.Paths[DataType.Character]);
            EnvironmentData = CustomResources.Load<EnvironmentData>(DataPath.Paths[DataType.Environment]);
        }

        protected override void Enable()
        {
            base.Enable();
        }

        protected override void Disable()
        {
            base.Disable();
            
            UnregisterManager(this);
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}