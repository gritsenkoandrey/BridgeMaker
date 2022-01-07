using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MGame : MonoBehaviour
    {
        public readonly ReactiveCommand OnRoundStart = new ReactiveCommand();
        public readonly ReactiveCommand OnRoundEnd = new ReactiveCommand();
    }
}