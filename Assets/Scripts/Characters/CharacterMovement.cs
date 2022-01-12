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

            Transform character = gameObject.transform;
            
            Vector2 i = Vector2.zero;
            float gravity = Physics.gravity.y * 10f;
            float speed = world.CurrentLevel.Value.GetSpeed;

            game.OnRoundStart
                .Subscribe(_ =>
                {
                    input.OnJoystickStart
                        .Subscribe(vector => { i = Vector2.zero; })
                        .AddTo(this);

                    input.OnJoystickHold
                        .Subscribe(vector => { i = vector; })
                        .AddTo(this);

                    input.OnJoystickEnd
                        .Subscribe(vector => { i = Vector2.zero; })
                        .AddTo(this);
                })
                .AddTo(this);

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Vector3 move = Vector3.zero;

                    if (i.magnitude > 0.05f)
                    {
                        move = new Vector3(i.x, 0f, i.y);

                        character.forward = move;

                        Vector3 next = character.position + move * speed * Time.deltaTime;

                        Ray ray = new Ray { origin = next, direction = Vector3.down };

                        if (!Physics.Raycast(ray, 1f, 1 << Layers.Ground)) return;
                    }

                    move.y = characterController.isGrounded ? 0f : gravity;

                    characterController.Move(move * speed * Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(this);
        }
    }
}