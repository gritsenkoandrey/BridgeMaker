using System;
using AssetPath;
using Cysharp.Threading.Tasks;
using Data;
using Environment.Collectors;
using Environment.Items;
using Environment.Platforms;
using Levels;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    public sealed class MWorld : BaseManager
    {
        private LevelData _levelData;
        private int _index;

        public readonly ReactiveProperty<Level> CurrentLevel = new ReactiveProperty<Level>();
        
        public readonly ReactiveCollection<Item> CharacterItems = new ReactiveCollection<Item>();
        public readonly ReactiveCollection<Item> ItemsColliders = new ReactiveCollection<Item>();
        public readonly ReactiveCollection<Platform> Platforms = new ReactiveCollection<Platform>();
        public readonly ReactiveCollection<Collector> CollectorsColliders = new ReactiveCollection<Collector>();

        protected override void Init()
        {
            base.Init();
            
            _index = PlayerPrefs.GetInt(U.Level, 0);
            _levelData = CustomResources.Load<LevelData>(DataPath.Paths[DataType.Level]);
        }

        protected override void Launch()
        {
            base.Launch();
        }

        protected override void Clear()
        {
            base.Clear();
            
            CharacterItems.Clear();
            ItemsColliders.Clear();
            Platforms.Clear();
            CollectorsColliders.Clear();
        }

        public async UniTask LoadLevel(bool isWin)
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

            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            
            CurrentLevel.SetValueAndForceNotify(SpawnLevel());
        }

        private Level SpawnLevel() => 
            Instantiate(_levelData.GetLevels[_index], Vector3.zero, Quaternion.identity);
    }
}