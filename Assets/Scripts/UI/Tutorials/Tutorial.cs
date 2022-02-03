using UniRx;
using UnityEngine;

namespace UI.Tutorials
{
    public abstract class Tutorial : MonoBehaviour
    {
        protected readonly ReactiveCommand onShowTutorial = new ReactiveCommand();

        protected readonly CompositeDisposable updateTutor = new CompositeDisposable();

        public void Show()
        {
            onShowTutorial.Execute();
        }

        public void Hide()
        {
            updateTutor.Clear();
        }
    }
}