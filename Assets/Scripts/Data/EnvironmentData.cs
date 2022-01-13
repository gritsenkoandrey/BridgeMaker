using Environment;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "Environment", order = 0)]
    public sealed class EnvironmentData : ScriptableObject
    {
        [SerializeField] private Item _item;

        public Item GetItem => _item;
    }
}