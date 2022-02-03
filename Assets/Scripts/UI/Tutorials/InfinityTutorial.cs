using UniRx;
using UnityEngine;

namespace UI.Tutorials
{
    public sealed class InfinityTutorial : Tutorial
    {
        [SerializeField] private RectTransform _fingerTransform;
        [SerializeField] private float _lemniscateRadius = 165f;
        [SerializeField] private float _height = 1f;
        [SerializeField] private float _speed = 3f;
        
        private void Awake()
        {
            onShowTutorial
                .Subscribe(_ =>
                {
                    Observable
                        .EveryUpdate()
                        .Subscribe(_ =>
                        {
                            float t = Time.time * _speed;

                            float x = _lemniscateRadius * Mathf.Sqrt(2) * Mathf.Cos(t);
                            float y = _lemniscateRadius * Mathf.Sqrt(2) * Mathf.Cos(t) * Mathf.Sin(t);

                            x /= Mathf.Pow(Mathf.Sin(t), 2) + 1;
                            y /= Mathf.Pow(Mathf.Sin(t), 2) + 1;
                            y *= _height;

                            _fingerTransform.localPosition = new Vector3(x, y, 0);
                        })
                        .AddTo(updateTutor)
                        .AddTo(this);
                })
                .AddTo(this);
        }
    }
}