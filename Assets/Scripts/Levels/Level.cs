using Characters;
using Cinemachine;
using Environment;
using Managers;
using UniRx;
using UnityEngine;

namespace Levels
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [Header("Prefabs")]
        [SerializeField] private Character _character;
        [SerializeField] private Item _item;
        [Header("Character Settings")]
        [SerializeField] private float _speed;

        private MGame _game;
        private MWorld _world;

        public CinemachineVirtualCamera GetCamera => _cinemachineVirtualCamera;
        public Character GetCharacter => _character;
        public Item GetItem => _item;
        public float GetSpeed => _speed;

        private void Awake()
        {
            _game = MContainer.Instance.GetGame;
            _world = MContainer.Instance.GetWorld;
        }

        private void Start()
        {
            _world.ItemsColliders
                .ObserveRemove()
                .Where(_ => _world.ItemsColliders.Count == 0)
                .First()
                .Subscribe(_ =>
                {
                    _game.OnRoundEnd.Execute(true);
                })
                .AddTo(this);
        }
    }
}