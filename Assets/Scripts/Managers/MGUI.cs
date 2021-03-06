using AssetPath;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public sealed class MGUI : BaseManager
    {
        public Image GetFade { get; private set; }
        public Canvas GetCanvas { get; private set; }

        protected override void Init()
        {
            base.Init();
            
            InstantiateCanvas();
            
            GetFade = GetCanvas.GetComponent<Image>();
        }

        protected override void Launch()
        {
            base.Launch();
        }

        protected override void Clear()
        {
            base.Clear();
        }

        private void InstantiateCanvas()
        {
            GetCanvas = Instantiate(CustomResources
                .Load<Canvas>(DataPath.Paths[DataType.Canvas]), gameObject.transform);
        }
    }
}