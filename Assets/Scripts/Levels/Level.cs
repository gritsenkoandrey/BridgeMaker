using APP;
using Cinemachine;
using Managers;
using UniRx;
using UnityEngine;

namespace Levels
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public CinemachineVirtualCamera GetCamera => _cinemachineVirtualCamera;

        private MGame _game;
        private MWorld _world;

        private void OnEnable()
        {
            _game = APPCore.Instance.game;
            _world = APPCore.Instance.world;
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