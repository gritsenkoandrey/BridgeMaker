using BaseMonoBehaviour;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Environment.Traps
{
    public sealed class Trap : BaseComponent
    {
        [SerializeField] private TrapSettings _trapSettings;
        [SerializeField] private Transform _model;

        private Tweener _tween;
        private Sequence _sequence;
        
        protected override void Init()
        {
            base.Init();

            switch (_trapSettings.type)
            {
                case TrapType.Barrier:
                    InitBarrier();
                    break;
                case TrapType.Canon:
                    InitCanon();
                    break;
                case TrapType.Cylinder:
                    InitCylinder();
                    break;
                case TrapType.SawHorizontal:
                    break;
                case TrapType.SawVertical:
                    InitSawVertical();
                    break;
                case TrapType.Spikes:
                    InitSpikes();
                    break;
            }
        }

        protected override void Enable()
        {
            base.Enable();
        }

        protected override void Disable()
        {
            base.Disable();
            
            _tween.KillTween();
            _sequence.KillTween();
        }

        private void InitBarrier()
        {
            _tween = _model
                .DOLocalRotate(Vector3.up * 360f, 1f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetDelay(_trapSettings.delay);
        }

        private void InitCylinder()
        {
            _tween = _model
                .DOLocalRotate(Vector3.up * 360f, 1f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetDelay(_trapSettings.delay);
        }

        private void InitSawVertical()
        {
            _sequence = _sequence.RefreshSequence();

            const float rotateTime = 1f;

            _sequence
                .Append(_model
                    .DOLocalMoveZ(_trapSettings.vector.z, _trapSettings.duration)
                    .SetRelative()
                    .SetEase(Ease.Linear))
                .Join(_model
                    .DOLocalRotate(Vector3.right * _trapSettings.vector.x * 360f, rotateTime, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear)
                    .SetLoops(Mathf.FloorToInt(_trapSettings.duration / rotateTime)))
                .AppendInterval(_trapSettings.delay)
                .Append(_model
                    .DOLocalMoveZ(-_trapSettings.vector.z, _trapSettings.duration)
                    .SetRelative()
                    .SetEase(Ease.Linear))
                .Join(_model
                    .DOLocalRotate(Vector3.right * -_trapSettings.vector.x * 360f, rotateTime, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear)
                    .SetLoops(Mathf.FloorToInt(_trapSettings.duration / rotateTime)))
                .AppendInterval(_trapSettings.delay)
                .SetLoops(-1);
        }

        private void InitSpikes()
        {
            _sequence = _sequence.RefreshSequence();

            _sequence
                .Append(_model.DOMove(_trapSettings.vector, _trapSettings.duration))
                .SetRelative()
                .SetEase(Ease.InBack)
                .AppendInterval(_trapSettings.delay)
                .Append(_model.DOMove(-_trapSettings.vector, _trapSettings.duration))
                .SetRelative()
                .SetEase(Ease.OutBack)
                .SetLoops(-1);
        }

        private void InitCanon()
        {
            _sequence = _sequence.RefreshSequence();

            _sequence
                .Append(_model.DOLocalRotate(_trapSettings.vector, _trapSettings.duration))
                .SetRelative()
                .SetEase(Ease.Linear)
                .AppendInterval(_trapSettings.delay)
                .Append(_model.DOLocalRotate(-_trapSettings.vector, _trapSettings.duration))
                .SetRelative()
                .SetEase(Ease.Linear)
                .AppendInterval(_trapSettings.delay)
                .SetLoops(-1);
        }
    }
}