using UnityEngine;

namespace Utils
{
    public static class Layers
    {
        private const string LayerGround = "Ground";
        private const string LayerPlayer = "Character";
        private const string LayerItem = "Item";
        private const string LayerDeactivate = "Deactivate";
        private const string LayerCollector = "Collector";

        public static int Ground { get; }
        public static int Character { get; }
        public static int Item { get; }
        public static int Deactivate { get; }
        public static int Collector { get; }

        static Layers()
        {
            Ground = LayerMask.NameToLayer(LayerGround);
            Character = LayerMask.NameToLayer(LayerPlayer);
            Item = LayerMask.NameToLayer(LayerItem);
            Deactivate = LayerMask.NameToLayer(LayerDeactivate);
            Collector = LayerMask.NameToLayer(LayerCollector);
        }
    }
}