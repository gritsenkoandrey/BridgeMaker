using DG.Tweening;

namespace Utils
{
    public static class DoTweenExtension
    {
        public static void KillTween(Tweener tween)
        {
            if (tween is { active: true })
            {
                tween.Kill();
            }
        }
        
        public static void KillTween(Sequence sequence)
        {
            if (sequence is { active: true })
            {
                sequence.Kill();
            }
        }
        
        public static Sequence RefreshSequence(Sequence sequence)
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