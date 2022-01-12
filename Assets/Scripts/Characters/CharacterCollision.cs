using System.Linq;
using Environment;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterCollision : Character
    {
        [SerializeField] private Transform _root;

        protected override void Init()
        {
            base.Init();

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

                    collector.disableNeighbors.Execute();

                    int count = world.CharacterItems.Count;
                    
                    if (count == 0) return;

                    int current = collector.Steps.Length - collector.index.Value;

                    if (count > current)
                    {
                        count = current;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = world.CharacterItems.GetLast();

                        item.onMove.Execute(collector);
                    }
                })
                .AddTo(this);
        }
    }
}