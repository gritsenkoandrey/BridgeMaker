using System.Linq;
using Environment.Collectors;
using Environment.Items;
using Managers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Characters
{
    public sealed class CharacterCollision : ICharacter
    {
        private readonly CharacterController _controller;
        private readonly Transform _root;

        private MWorld _world;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterCollision(CharacterController controller, Transform root)
        {
            _controller = controller;
            _root = root;
        }
        
        public void Register()
        {
            _world = Manager.Resolve<MWorld>();
            
            _controller
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Item)
                .Subscribe(col =>
                {
                    Item item = _world.ItemsColliders
                        .FirstOrDefault(i => i.gameObject.Equals(col.gameObject));
                    
                    if (!item) return;

                    item.onPick.Execute(_root);
                })
                .AddTo(_disposable);
            
            _controller
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Collector)
                .Subscribe(col =>
                {
                    Collector collector = _world.CollectorsColliders
                        .FirstOrDefault(c => c.gameObject.Equals(col.gameObject));
                    
                    if (!collector) return;

                    int count = _world.CharacterItems.Count;
                    
                    if (count == 0) return;

                    int current = collector.Steps.Length - collector.index.Value;
                    
                    if (count > current)
                    {
                        count = current;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = _world.CharacterItems.GetLast();

                        item.onMove.Execute(collector);
                    }
                })
                .AddTo(_disposable);
        }

        public void Unregistered()
        {
            _disposable.Clear();
        }
    }
}