using Environment;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private Transform _root;
        
        private MWorld _world;

        private void Awake()
        {
            _world = MContainer.Instance.GetWorld;
        }

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Item item = Instantiate(_world.CurrentLevel.Value.GetItem, transform.GetChild(i).position, Quaternion.identity, _root);
                item.GetRenderer.material.color = _color;
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