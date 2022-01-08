using UI.Enum;
using UI.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public sealed class MGUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _fade;

        public Transform GetRoot => _canvas.transform;
        public Image GetFade => _fade;

        private void Start()
        {
            ScreenInterface.GetScreenInterface().Execute(ScreenType.LobbyScreen);
        }
    }
}