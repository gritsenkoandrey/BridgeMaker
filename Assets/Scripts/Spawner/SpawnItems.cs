using Environment.Items;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private Transform _root;

        private MConfig _config;

        private void Awake()
        {
            _config = Manager.Resolve<MConfig>();
        }

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Item item = Instantiate(_config.EnvironmentData.GetItem, transform.GetChild(i).position, Quaternion.identity, _root);

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