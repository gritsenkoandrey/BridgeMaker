using Levels;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level", order = 0)]
    public sealed class LevelData : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public Level[] GetLevels => _levels;
    }
}