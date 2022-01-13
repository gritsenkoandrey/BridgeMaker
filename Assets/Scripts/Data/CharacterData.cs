using Characters;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character", order = 0)]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField] private Character _character;

        public Character GetCharacter => _character;
    }
}