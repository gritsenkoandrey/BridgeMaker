using DG.Tweening;

namespace Utils
{
    public static class DoTweenExtension
    {
        public static void KillTween(this Tweener tween)
        {
            if (tween is { active: true })
            {
                tween.Kill();
            }
        }
        
        public static void KillTween(this Sequence sequence)
        {
            if (sequence is { active: true })
            {
                sequence.Kill();
            }
        }
        
        public static Sequence RefreshSequence(this Sequence sequence)
        {
            if (sequence is { active: true })
            {
                sequence.Kill();
            }
            
            sequence = DOTween.Sequence();
            
            return sequence;
        }
    }
}