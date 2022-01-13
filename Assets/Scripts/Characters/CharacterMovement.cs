using UniRx;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterMovement : Character
    {
        protected override void Init()
        {
            base.Init();

            Transform character = transform;
            
            Vector2 input = Vector2.zero;
            float gravity = Physics.gravity.y * 10f;
            float speed = GetWorld.CurrentLevel.Value.GetSpeed;

            GetGame.OnRoundStart
                .Subscribe(_ =>
                {
                    GetInput.OnJoystickStart
                        .Subscribe(vector => { input = Vector2.zero; })
                        .AddTo(this);

                    GetInput.OnJoystickHold
                        .Subscribe(vector => { input = vector; })
                        .AddTo(this);

                    GetInput.OnJoystickEnd
                        .Subscribe(vector => { input = Vector2.zero; })
                        .AddTo(this);
                })
                .AddTo(this);

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Vector3 move = Vector3.zero;

                    if (input.magnitude > 0.1f)
                    {
                        move = new Vector3(input.x, 0f, input.y);

                        character.forward = move;

                        Vector3 next = character.position + move * speed * Time.deltaTime;

                        Ray ray = new Ray { origin = next, direction = Vector3.down };

                        if (!Physics.Raycast(ray, 1f, 1 << Layers.Ground)) return;
                    }

                    move.y = Controller.isGrounded ? 0f : gravity;

                    Controller.Move(move * speed * Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(lifetimeDisposable);
        }
    }
}