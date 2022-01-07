using System.Linq;
using APP;
using Environment;
using Managers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterCollision : MonoBehaviour
    {
        private MWorld _world;
        
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _root;

        private void OnEnable()
        {
            _world = APPCore.Instance.world;
        }

        private void Start()
        {
            _collider
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Item)
                .Subscribe(col =>
                {
                    Item item = _world.ItemsColliders
                        .FirstOrDefault(i => i.gameObject.Equals(col.gameObject));
                    
                    if (!item) return;

                    item.onPick.Execute(_root);
                })
                .AddTo(this);
            
            _collider
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Collector)
                .Subscribe(col =>
                {
                    Collector collector = _world.CollectorsColliders
                            .FirstOrDefault(c => c.gameObject.Equals(col.gameObject));
                    
                    if (!collector) return;

                    int count = _world.CharacterItems.Count;
                    
                    if (count == 0) return;

                    int current = collector.steps.Count - collector.index.Value;

                    if (count > current)
                    {
                        count = current;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = _world.CharacterItems.Last();

                        item.onMove.Execute(collector);
                    }
                })
                .AddTo(this);
        }
    }
}