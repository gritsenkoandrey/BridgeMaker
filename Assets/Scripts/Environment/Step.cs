using BaseMonoBehaviour;
using UnityEngine;

namespace Environment
{
    public sealed class Step : BaseComponent
    {
        [SerializeField] private Renderer _renderer;

        public Renderer GetRenderer => _renderer;
    }
}