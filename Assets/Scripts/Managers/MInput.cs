using SimpleInputNamespace;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        
        public readonly BoolReactiveProperty IsEnable = new BoolReactiveProperty();

        public readonly ReactiveProperty<Vector2> OnJoystickStart = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickHold = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<Vector2> OnJoystickEnd = new ReactiveProperty<Vector2>();

        private readonly CompositeDisposable _inputDisposable = new CompositeDisposable();

        private void OnEnable()
        {
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
                .AddTo(this);
        }

        private void OnDisable()
        {
            _inputDisposable.Clear();
        }

        private void Start()
        {
            IsEnable.SetValueAndForceNotify(true);
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