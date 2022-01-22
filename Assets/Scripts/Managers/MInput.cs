using SimpleInputNamespace;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MInput : Manager
    {
        [SerializeField] private Joystick _joystick;
        
        public readonly BoolReactiveProperty IsEnable = new BoolReactiveProperty();

        public readonly ReactiveProperty<Vector2> OnJoystickStart = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickHold = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickEnd = new ReactiveProperty<Vector2>();

        private readonly CompositeDisposable _inputDisposable = new CompositeDisposable();

        protected override void Register()
        {
            RegisterManager(this);
        }

        protected override void Enable()
        {
            base.Enable();
            
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
                .AddTo(managerDisposable);
        }

        protected override void Disable()
        {
            base.Disable();

            Clear();
            
            UnregisterManager(this);
        }
        
        protected override void Init()
        {
            base.Init();
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

        private void Clear()
        {
            _inputDisposable.Clear();
        }
    }
}