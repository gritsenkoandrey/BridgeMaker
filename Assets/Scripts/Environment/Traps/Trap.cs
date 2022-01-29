using System;
using System.Collections.Generic;
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

        private Dictionary<TrapType, Action> _initTrap;

        private Sequence _sequence;

        protected override void Enable()
        {
            base.Enable();

            _initTrap = new Dictionary<TrapType, Action>
            {
                { TrapType.None, () => Debug.Log("None")},
                { TrapType.Barrier, InitBarrier},
                { TrapType.Canon, InitCanon},
                { TrapType.Cylinder, InitCylinder},
                { TrapType.SawVertical, InitSawVertical},
                { TrapType.Spikes, InitSpikes},
                { TrapType.SawHorizontal, InitSawHorizontal}
            };
        }

        protected override void Disable()
        {
            base.Disable();
            
            _sequence.KillTween();
        }

        protected override void Init()
        {
            base.Init();
            
            _sequence = _sequence.RefreshSequence();
            
            _initTrap[_trapSettings.type].Invoke();
        }

        private void InitBarrier()
        {
            _sequence
                .Append(_model
                    .DOLocalRotate(Vector3.up * 360f, _trapSettings.duration, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear))
                .SetLoops(-1);
        }

        private void InitCylinder()
        {
            _sequence
                .Append(_model
                    .DOLocalRotate(Vector3.up * 360f, _trapSettings.duration, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear))
                .SetLoops(-1);
        }

        private void InitSawVertical()
        {
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

        private void InitSawHorizontal()
        {
            _sequence
                .Append(_model
                    .DOLocalRotate(Vector3.up * 360f, _trapSettings.duration, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear))
                .SetLoops(-1);
        }
    }
}