using APP;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnCharacter : MonoBehaviour
    {
        private MWorld _world;

        private void Awake()
        {
            _world = APPCore.Instance.GetWorld;
        }

        private void Start()
        {
            Transform character = Instantiate(_world.CurrentLevel.Value.GetCharacter, transform.position, Quaternion.identity,
                _world.CurrentLevel.Value.transform).transform;

            _world.CurrentLevel.Value.GetCamera.Follow = character;
            _world.CurrentLevel.Value.GetCamera.LookAt = character;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(transform.position, 0.75f);
        }
    }
}