using AssetPath;
using Data;
using Utils;

namespace Managers
{
    public sealed class MConfig : Manager
    {
        public CharacterData CharacterData { get; private set; }
        public EnvironmentData EnvironmentData { get; private set; }
        public SettingsData SettingsData { get; private set; }
        
        protected override void Register()
        {
            RegisterManager(this);
        }

        protected override void Enable()
        {
            base.Enable();
            
            CharacterData = CustomResources.Load<CharacterData>(DataPath.Paths[DataType.Character]);
            EnvironmentData = CustomResources.Load<EnvironmentData>(DataPath.Paths[DataType.Environment]);
            SettingsData = CustomResources.Load<SettingsData>(DataPath.Paths[DataType.Settings]);
        }

        protected override void Disable()
        {
            base.Disable();
            
            UnregisterManager(this);
        }

        protected override void Init()
        {
            base.Init();
        }
    }
}