using Environment;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MGame : MonoBehaviour
    {
        public readonly ReactiveCollection<Item> Items = new ReactiveCollection<Item>();
    }
}