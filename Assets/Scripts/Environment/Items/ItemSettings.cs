using UnityEngine;

namespace Environment.Items
{
    [System.Serializable]
    public struct ItemSettings
    {
        public ItemType itemType;
        public Vector3 vector;
        public Color color;
        public float duration;
        public float delay;
    }
}