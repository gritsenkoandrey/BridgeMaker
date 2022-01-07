using APP;
using Environment;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        private MWorld _world;
        
        [SerializeField] private Color _color;
        [SerializeField] private Item _item;

        private void OnEnable()
        {
            _world = APPCore.Instance.world;
        }

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Item item = Instantiate(_item, transform.GetChild(i).position, Quaternion.identity, _world.CurrentLevel.Value.transform);
                item.GetRenderer.material.color = _color;
                
                _world.ItemsColliders.Add(item);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;

            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawCube(transform.GetChild(i).position, Vector3.one * 0.25f);
            }
        }
    }
}