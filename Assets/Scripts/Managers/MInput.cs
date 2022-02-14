using SimpleInputNamespace;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MInput : BaseManager
    {
        [SerializeField] private Joystick _joystick;
        
        public readonly BoolReactiveProperty IsEnable = new BoolReactiveProperty();

        public readonly ReactiveProperty<Vector2> OnJoystickStart = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickHold = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickEnd = new ReactiveProperty<Vector2>();

        private readonly CompositeDisposable _inputDisposable = new CompositeDisposable();

        protected override void Init()
        {
            base.Init();
            
            IsEnable
                .Subscribe(value =>
                {
                    if (value)
                    { 
                        Observable
                            .EveryUpdate()
                            .Subscribe(_ =>
                            {
                                UpdateJoystick(_joystick);
                            })
                            .AddTo(_inputDisposable);
                    }
                    else
                    {
                        _inputDisposable.Clear();
                    }
                })
                .AddTo(ManagerDisposable);
        }

        protected override void Launch()
        {
            base.Launch();
        }

        protected override void Clear()
        {
            base.Clear();
            
            _inputDisposable.Clear();
        }

        private void UpdateJoystick(Joystick joystick)
        {
            Vector2 input = joystick.Value;
        
            if (Input.GetMouseButtonDown(0))
            {
                JoystickStart(input);
            }
            else if (Input.GetMouseButton(0))
            {
                JoystickHold(input);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                JoystickEnd(input);
            }
        }

        private void JoystickStart(Vector2 input)
        {
            OnJoystickStart.SetValueAndForceNotify(input);
        }
        
        private void JoystickHold(Vector2 input)
        {
            OnJoystickHold.SetValueAndForceNotify(input);
        }
        
        private void JoystickEnd(Vector2 input)
        {
            OnJoystickEnd.SetValueAndForceNotify(input);
        }
    }
}