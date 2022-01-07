using UnityEngine;

namespace Utils
{
    public static class Animations
    {
        private const string AnimationRun = "Run";
        private const string AnimationVictory = "Victory";
        
        public static int Run { get; }
        public static int Victory { get; }

        static Animations()
        {
            Run = Animator.StringToHash(AnimationRun);
            Victory = Animator.StringToHash(AnimationVictory);
        }
    }
}