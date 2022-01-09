using UniRx;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterMovement : Character
    {
        [SerializeField] private float _speed;

        protected override void Initialize()
        {
            base.Initialize();

            Transform character = gameObject.transform;
            
            Vector2 i = Vector2.zero;
            float gravity = Physics.gravity.y * 10f;

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

                    if (i.magnitude > 0.1f)
                    {
                        move = new Vector3(i.x, 0f, i.y);

                        character.forward = move;

                        Vector3 next = character.position + move * _speed * Time.deltaTime;

                        Ray ray = new Ray { origin = next, direction = Vector3.down };

                        if (!Physics.Raycast(ray, 1f, 1 << Layers.Ground)) return;
                    }

                    move.y = characterController.isGrounded ? 0f : gravity;

                    characterController.Move(move * _speed * Time.deltaTime);
                })
                .AddTo(characterDisposable)
                .AddTo(this);
        }
    }
}