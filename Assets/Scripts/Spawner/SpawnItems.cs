using AssetPath;
using Data;
using Environment;
using UnityEngine;
using Utils;

namespace Spawner
{
    public sealed class SpawnItems : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private Transform _root;

        private void Start()
        {
            Item prefab = CustomResources.Load<EnvironmentData>(DataPath.paths[DataType.Environment]).GetItem;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Item item = Instantiate(prefab, transform.GetChild(i).position, Quaternion.identity, _root);
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