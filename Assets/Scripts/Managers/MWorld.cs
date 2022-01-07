using Environment;
using Data;
using Levels;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MWorld : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;

        private int _index;

        public readonly ReactiveProperty<Level> CurrentLevel = new ReactiveProperty<Level>();
        
        public readonly ReactiveCollection<Item> CharacterItems = new ReactiveCollection<Item>();
        public readonly ReactiveCollection<Item> ItemsColliders = new ReactiveCollection<Item>();
        
        [HideInInspector] public ReactiveCollection<Collector> CollectorsColliders = new ReactiveCollection<Collector>();

        private void Start()
        {
            _index = PlayerPrefs.GetInt("Level", 0);
            
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

                    if (_index == levelData.GetLevels.Length)
                    {
                        _index = 0;
                    }

                    PlayerPrefs.SetInt("Level", _index);
                    PlayerPrefs.Save();
                }
            }
            
            CurrentLevel.SetValueAndForceNotify(Instantiate(levelData.GetLevels[_index], Vector3.zero, Quaternion.identity));

            CollectorsColliders = CurrentLevel.Value.GetComponentsInChildren<Collector>().ToReactiveCollection();
        }
    }
}