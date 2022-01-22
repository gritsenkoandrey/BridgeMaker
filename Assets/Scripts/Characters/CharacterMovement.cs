using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterMovement : CharacterBase
    {
        protected override void Init()
        {
            base.Init();

            Transform character = transform;
            
            Vector2 joystick = Vector2.zero;
            float gravity = Physics.gravity.y * 10f;
            float speed = config.CharacterData.GetSpeed;
            
            input.OnJoystickStart
                .Subscribe(vector => joystick = Vector2.zero)
                .AddTo(lifetimeDisposable);

            input.OnJoystickHold
                .Subscribe(vector => joystick = vector)
                .AddTo(lifetimeDisposable);

            input.OnJoystickEnd
                .Subscribe(vector => joystick = Vector2.zero)
                .AddTo(lifetimeDisposable);

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

                        if (!Physics.Raycast(ray, 1f, 1 << Layers.Ground)) return;
                    }

                    move.y = characterController.isGrounded ? 0f : gravity;

                    characterController.Move(move * speed * Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();

            characterController = GetComponent<CharacterController>();
        }
    }
}