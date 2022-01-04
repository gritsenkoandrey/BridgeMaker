using LevelData;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MWorld : MonoBehaviour
    {
        [SerializeField] private Level level;

        public readonly ReactiveProperty<Level> CurrentLevel = new ReactiveProperty<Level>();

        private void Start()
        {
            CurrentLevel.SetValueAndForceNotify(Instantiate(level, Vector3.zero, Quaternion.identity));
        }
    }
}