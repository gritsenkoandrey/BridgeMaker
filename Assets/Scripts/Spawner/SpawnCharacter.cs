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
            
            Character character = Instantiate(_config.CharacterData.GetCharacter, transform.position, Quaternion.identity, level.transform);
            character.Construct();

            level.GetCamera.Follow = character.transform;
            level.GetCamera.LookAt = character.transform;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(transform.position, 0.75f);
        }
    }
}