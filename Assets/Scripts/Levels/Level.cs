using Cinemachine;
using Managers;
using UniRx;
using UnityEngine;

namespace Levels
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [Header("Character Settings")]
        [SerializeField] private float _speed;

        private MGame _game;
        private MWorld _world;

        public CinemachineVirtualCamera GetCamera => _cinemachineVirtualCamera;
        public float GetSpeed => _speed;

        private void Awake()
        {
            _game = Manager.Resolve<MGame>();
            _world = Manager.Resolve<MWorld>();
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