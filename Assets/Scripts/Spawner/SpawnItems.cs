using Environment.Items;
using Environment.Platforms;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private Platform _platform;

        private MConfig _config;

        private void Awake()
        {
            _config = Manager.Resolve<MConfig>();
        }

        private void Start()
        {
            _platform.Count = transform.childCount;
            
            for (int i = 0; i < _platform.Count; i++)
            {
                Item item = Instantiate(_config.EnvironmentData.GetItem, 
                    transform.GetChild(i).position, Quaternion.identity, _platform.transform);

                ItemSettings temp = new ItemSettings
                {
                    color = _itemSettings.color,
                    vector = _itemSettings.vector,
                    itemType = _itemSettings.itemType,
                    delay = _itemSettings.delay * i,
                    duration = _itemSettings.duration
                };

                item.ItemSettings = temp;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _itemSettings.color;

            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawCube(transform.GetChild(i).position, Vector3.one * 0.25f);
            }
        }
    }
}