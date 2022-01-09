﻿using AssetPath;
using Environment;
using Data;
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
        
        protected override void First()
        {
            Container.Add(typeof(MWorld), this);
        }

        protected override void Init()
        {
            _index = PlayerPrefs.GetInt(U.Level, 0);

            _levelData = CustomResources.Load<LevelData>(DataPath.paths[DataType.Level]);
            
            InstantiateLevel();
        }

        public void InstantiateLevel(bool win = false)
        {
            CharacterItems.Clear();
            ItemsColliders.Clear();
            CollectorsColliders.Clear();

            if (CurrentLevel.Value)
            {
                Destroy(CurrentLevel.Value.gameObject);

                if (win)
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
            
            CurrentLevel.SetValueAndForceNotify(Instantiate(_levelData.GetLevels[_index], Vector3.zero, Quaternion.identity));
        }
    }
}