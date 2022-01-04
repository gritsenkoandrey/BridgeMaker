using UnityEngine;

namespace Utils
{
    public static class Animations
    {
        private const string AnimationRun = "Run";
        
        public static int Run { get; }

        static Animations()
        {
            Run = Animator.StringToHash(AnimationRun);
        }
    }
}