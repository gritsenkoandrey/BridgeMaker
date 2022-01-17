using Characters;
using Levels;
using Managers;
using UnityEngine;

namespace Spawner
{
    public sealed class SpawnCharacter : MonoBehaviour
    {
        private MConfig _config;

        private void Awake()
        {
            _config = Manager.Resolve<MConfig>();
        }

        private void Start()
        {
            Level level = Manager.Resolve<MWorld>().CurrentLevel.Value;
            
            CharacterBase prefab = _config.CharacterData.GetCharacter;
            
            Transform character = Instantiate(prefab, transform.position, Quaternion.identity, level.transform).transform;

            level.GetCamera.Follow = character;
            level.GetCamera.LookAt = character;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(transform.position, 0.75f);
        }
    }
}