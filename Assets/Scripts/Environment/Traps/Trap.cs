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
        
        protected override void Init()
        {
            base.Init();

            switch (_trapSettings.type)
            {
                case TrapType.Barrier:
                    InitBarrier();
                    break;
                case TrapType.Canon:
                    break;
                case TrapType.Cylinder:
                    break;
                case TrapType.SawHorizontal:
                    break;
                case TrapType.SawVertical:
                    break;
                case TrapType.Spikes:
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
        }

        private void InitBarrier()
        {
            _tween = _model
                .DOLocalRotate(_trapSettings.vector * 360f, _trapSettings.duration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetDelay(_trapSettings.delay);
        }
    }
}