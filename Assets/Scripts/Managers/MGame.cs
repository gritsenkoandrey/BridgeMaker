using DG.Tweening;
using UI.Enum;
using UI.Factory;
using UniRx;
using UnityEngine;

namespace Managers
{
    public sealed class MGame : MonoBehaviour
    {
        public readonly ReactiveCommand OnRoundStart = new ReactiveCommand();
        public readonly ReactiveCommand<bool> OnRoundEnd = new ReactiveCommand<bool>();

        private void Start()
        {
            OnRoundEnd
                .Subscribe(value =>
                {
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        ScreenInterface.GetScreenInterface()
                            .Execute(value ? ScreenType.WinScreen : ScreenType.LoseScreen);
                    });
                })
                .AddTo(this);
        }
    }
}