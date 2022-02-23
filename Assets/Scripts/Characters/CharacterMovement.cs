using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterMovement : ICharacter
    {
        private readonly CharacterController _controller;

        private MInput _input;
        private MConfig _config;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterMovement(CharacterController controller)
        {
            _controller = controller;
        }
        
        public void Register()
        {
            _input = Manager.Resolve<MInput>();
            _config = Manager.Resolve<MConfig>();
            
            Vector2 joystick = Vector2.zero;

            Transform character = _controller.transform;
            
            float gravity = Physics.gravity.y * 10f;
            float speed = _config.CharacterData.GetCharacterSettings.speed;
            
            _input.OnJoystickStart
                .Subscribe(vector => joystick = Vector2.zero)
                .AddTo(_disposable);

            _input.OnJoystickHold
                .Subscribe(vector => joystick = vector)
                .AddTo(_disposable);

            _input.OnJoystickEnd
                .Subscribe(vector => joystick = Vector2.zero)
                .AddTo(_disposable);

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Vector3 move = Vector3.zero;

                    if (joystick.magnitude > 0.1f)
                    {
                        move = new Vector3(joystick.x, 0f, joystick.y);

                        character.forward = move;

                        Vector3 next = character.position + move * speed * Time.deltaTime;

                        Ray ray = new Ray { origin = next, direction = Vector3.down };

                        if (!Physics.Raycast(ray, 1f, Layers.Ground)) return;
                    }

                    move.y = _controller.isGrounded ? 0f : gravity;

                    _controller.Move(move * speed * Time.deltaTime);
                })
                .AddTo(_disposable);
        }

        public void Unregistered()
        {
            _disposable.Clear();
        }
    }
}