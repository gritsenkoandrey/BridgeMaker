using APP;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private MInput _input;

        private readonly ReactiveCommand<Vector2> _onMove = new ReactiveCommand<Vector2>();

        private void OnEnable()
        {
            _input = APPCore.Instance.input;
        }

        private void Start()
        {
            Vector2 input = Vector2.zero;

            Transform character = gameObject.transform;
            float gravity = Physics.gravity.y * 10f;
            float speed = 5f;

            _input.OnJoystickStart
                .Subscribe(vector =>
                {
                    input = Vector2.zero;
                })
                .AddTo(this);

            _input.OnJoystickHold
                .Subscribe(vector =>
                {
                    input = vector;
                })
                .AddTo(this);

            _input.OnJoystickEnd
                .Subscribe(vector =>
                {
                    input = Vector2.zero;
                })
                .AddTo(this);

            _onMove
                .Subscribe(vector =>
                {
                    Vector3 move = Vector3.zero;

                    if (vector.magnitude > 0.1f)
                    {
                        move = new Vector3(vector.x, 0f, vector.y);
                        character.forward = move;

                        Vector3 next = character.position + move * speed * Time.deltaTime;

                        Ray ray = new Ray { origin = next, direction = Vector3.down };
                        
                        if (!Physics.Raycast(ray, 1f, 1 << Layers.Ground)) return;
                    }

                    move.y = _characterController.isGrounded ? 0f : gravity;

                    _characterController.Move(move * speed * Time.deltaTime);
                })
                .AddTo(this);

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _onMove.Execute(input);
                })
                .AddTo(this);
        }
    }
}