using Characters;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character", order = 0)]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterBase _character;
        [SerializeField] private CharacterSettings _characterSettings;

        public CharacterBase GetCharacter => _character;
        public CharacterSettings GetCharacterSettings => _characterSettings;
    }
}