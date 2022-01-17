using Characters;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character", order = 0)]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterBase _character;
        [SerializeField] private float _speed;

        public CharacterBase GetCharacter => _character;
        public float GetSpeed => _speed;
    }
}