using UnityEngine;

namespace Environment
{
    public sealed class Step : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        public Renderer GetRenderer => _renderer;
    }
}