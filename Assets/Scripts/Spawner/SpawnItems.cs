using AssetPath;
using Data;
using Environment.Items;
using UnityEngine;
using Utils;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private Transform _root;

        private void Start()
        {
            Item prefab = CustomResources.Load<EnvironmentData>(DataPath.Paths[DataType.Environment]).GetItem;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Item item = Instantiate(prefab, transform.GetChild(i).position, Quaternion.identity, _root);

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