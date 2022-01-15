using AssetPath;
using Characters;
using Data;
using Levels;
using Managers;
using UnityEngine;
using Utils;

namespace Spawner
{
    public sealed class SpawnCharacter : MonoBehaviour
    {
        private void Start()
        {
            Level level = Manager.Resolve<MWorld>().CurrentLevel.Value;
            
            CharacterBase prefab = CustomResources.Load<CharacterData>(DataPath.paths[DataType.Character]).GetCharacter;
            
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