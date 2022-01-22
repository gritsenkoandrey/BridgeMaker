using AssetPath;
using Data;
using Environment.Collectors;
using Environment.Items;
using Levels;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    public sealed class MWorld : Manager
    {
        private LevelData _levelData;
        private int _index;

        public readonly ReactiveProperty<Level> CurrentLevel = new ReactiveProperty<Level>();
        
        public readonly ReactiveCollection<Item> CharacterItems = new ReactiveCollection<Item>();
        public readonly ReactiveCollection<Item> ItemsColliders = new ReactiveCollection<Item>();
        public readonly ReactiveCollection<Collector> CollectorsColliders = new ReactiveCollection<Collector>();
        
        protected override void Register()
        {
            RegisterManager(this);
        }

        protected override void Enable()
        {
            base.Enable();
            
            _index = PlayerPrefs.GetInt(U.Level, 0);
            _levelData = CustomResources.Load<LevelData>(DataPath.Paths[DataType.Level]);
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

        public void LoadLevel(bool isWin)
        {
            Clear();

            if (CurrentLevel.Value)
            {
                Destroy(CurrentLevel.Value.gameObject);

                if (isWin)
                {
                    _index++;

                    if (_index == _levelData.GetLevels.Length)
                    {
                        _index = 0;
                    }

                    PlayerPrefs.SetInt(U.Level, _index);
                    PlayerPrefs.Save();
                }
            }

            CurrentLevel.SetValueAndForceNotify(SpawnLevel());
        }

        private Level SpawnLevel()
        {
            return Instantiate(_levelData.GetLevels[_index], Vector3.zero, Quaternion.identity);
        }

        private void Clear()
        {
            CharacterItems.Clear();
            ItemsColliders.Clear();
            CollectorsColliders.Clear();
        }
    }
}