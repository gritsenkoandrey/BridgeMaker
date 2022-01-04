using UniRx;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _animator.SetFloat(Animations.Run, _characterController.velocity.magnitude, 0.05f, Time.deltaTime);
                })
                .AddTo(this);
        }
    }
}