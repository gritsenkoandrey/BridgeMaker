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
        private MGame _game;
        
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _root;

        private void OnEnable()
        {
            _game = APPCore.Instance.game;
        }

        private void Start()
        {
            _collider
                .OnTriggerEnterAsObservable()
                .Where(col => col.gameObject.layer == Layers.Item)
                .Subscribe(c =>
                {
                    Item item = c.GetComponent<Item>();
                    
                    if (!item) return;

                    item.OnPick.Execute(_root);
                })
                .AddTo(this);
            
            _collider
                .OnTriggerEnterAsObservable()
                .Where(col => col.gameObject.layer == Layers.Collector)
                .Subscribe(c =>
                {
                    Collector collector = c.GetComponent<Collector>();

                    Color color = Color.white;
                    
                    if (!collector) return;

                    int count = _game.Items.Count;
                    
                    if (count == 0) return;

                    int cur = collector.steps.Count - collector.index;

                    if (count > cur)
                    {
                        count = cur;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = _game.Items.Last();

                        item.OnMove.Execute(collector);

                        color = item.GetRenderer.material.color;
                    }

                    if (collector.steps.Count == collector.index)
                    {
                        collector.gameObject.layer = Layers.Deactivate;
                        collector.OnPaintRoad.Execute(color);
                    }
                })
                .AddTo(this);
        }
    }
}