using System.Linq;
using Environment;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Character
{
    public sealed class CharacterCollision : Character
    {
        [SerializeField] private Transform _root;

        protected override void Initialize()
        {
            base.Initialize();

            characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Item)
                .Subscribe(col =>
                {
                    Item item = world.ItemsColliders
                        .FirstOrDefault(i => i.gameObject.Equals(col.gameObject));
                    
                    if (!item) return;

                    item.onPick.Execute(_root);
                })
                .AddTo(this);
            
            characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Collector)
                .Subscribe(col =>
                {
                    Collector collector = world.CollectorsColliders
                        .FirstOrDefault(c => c.gameObject.Equals(col.gameObject));
                    
                    if (!collector) return;

                    int count = world.CharacterItems.Count;
                    
                    if (count == 0) return;

                    int current = collector.steps.Count - collector.index.Value;

                    if (count > current)
                    {
                        count = current;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = world.CharacterItems.Last();

                        item.onMove.Execute(collector);
                    }
                })
                .AddTo(this);
        }
    }
}