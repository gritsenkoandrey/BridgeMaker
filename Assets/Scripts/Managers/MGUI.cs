using AssetPath;
using UI.Enum;
using UI.Factory;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public sealed class MGUI : Manager
    {
        public Canvas GetCanvas { get; private set; }
        public Image GetImage { get; private set; }

        protected override void Register()
        {
           RegisterManager(this);
        }
        
        protected override void Disable()
        {
            UnregisterManager(this);
        }

        protected override void Init()
        {
            InitCanvas();
            
            GetImage = GetCanvas.GetComponent<Image>();

            ScreenInterface.GetScreenInterface().Execute(ScreenType.LobbyScreen);
        }

        private void InitCanvas() => 
            GetCanvas = Instantiate(CustomResources.Load<Canvas>(DataPath.Paths[DataType.Canvas]), gameObject.transform);
    }
}