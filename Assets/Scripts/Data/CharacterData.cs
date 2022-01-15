using Characters;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character", order = 0)]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterBase _character;

        public CharacterBase GetCharacter => _character;
    }
}