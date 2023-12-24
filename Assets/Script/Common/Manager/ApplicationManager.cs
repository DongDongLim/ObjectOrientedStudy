using System;
using UniRx;


namespace Study.Core
{
    public class ApplicationManager : SingletonMonoBehaviour<ApplicationManager>
    {
        private Subject<bool> _applicationPauseSubject;
        private Subject<bool> _applicationFocusSubject;
        private Subject<Unit> _applicationQuitSubject;

        public IObservable<bool> OnApplicationPauseObservable => _applicationPauseSubject;
        public IObservable<bool> OnApplicationFocusObservable => _applicationFocusSubject;
        public IObservable<Unit> OnApplicationQuitObservable => _applicationQuitSubject;

        protected override void Initialize()
        {
            _applicationPauseSubject = new Subject<bool>();
            _applicationFocusSubject = new Subject<bool>();
            _applicationQuitSubject = new Subject<Unit>();
        }

        private void OnApplicationPause(bool pause)
        {
            _applicationPauseSubject.OnNext(pause);
        }

        private void OnApplicationFocus(bool focus)
        {
            _applicationFocusSubject.OnNext(focus);
        }

        private void OnApplicationQuit()
        {
            _applicationQuitSubject.OnNext(Unit.Default);
        }

    }
}