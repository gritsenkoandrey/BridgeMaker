using AssetPath;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public sealed class MGUI : Manager
    {
        public Image GetFade { get; private set; }
        public Canvas GetCanvas { get; private set; }

        protected override void Register()
        {
           RegisterManager(this);
        }

        protected override void Enable()
        {
            base.Enable();
            
            InstantiateCanvas();
            
            GetFade = GetCanvas.GetComponent<Image>();
        }

        protected override void Disable()
        {
            base.Disable();

            UnregisterManager(this);
        }

        protected override void Init()
        {
            base.Init();
        }

        private void InstantiateCanvas()
        {
            GetCanvas = Instantiate(CustomResources
                .Load<Canvas>(DataPath.Paths[DataType.Canvas]), gameObject.transform);
        }
    }
}