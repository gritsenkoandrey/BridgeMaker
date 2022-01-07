using UnityEngine;

namespace Utils
{
    public static class U
    {
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
        {
            return Mathf.Lerp(oMin, oMax, Mathf.InverseLerp(iMin, iMax, value));
        }

        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}