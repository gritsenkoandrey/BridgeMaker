using UnityEngine;

namespace Utils
{
    public static class Layers
    {
        private const string LayerGround = "Ground";
        private const string LayerCharacter = "Character";
        private const string LayerItem = "Item";
        private const string LayerDeactivate = "Deactivate";
        private const string LayerCollector = "Collector";
        private const string LayerTrap = "Trap";

        public static int Ground { get; }
        public static int Character { get; }
        public static int Item { get; }
        public static int Deactivate { get; }
        public static int Collector { get; }
        public static int Trap { get; }

        static Layers()
        {
            Ground = LayerMask.GetMask(LayerGround);
            
            Character = LayerMask.NameToLayer(LayerCharacter);
            Item = LayerMask.NameToLayer(LayerItem);
            Deactivate = LayerMask.NameToLayer(LayerDeactivate);
            Collector = LayerMask.NameToLayer(LayerCollector);
            Trap = LayerMask.NameToLayer(LayerTrap);
        }
    }
}