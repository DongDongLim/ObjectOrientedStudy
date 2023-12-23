using System;
using UniRx;


namespace Study.Core
{
    public class ApplicationManager : SingletonMonoBehaviour<ApplicationManager>
    {
        private Subject<bool> _applicationPauseSubject;

        public IObservable<bool> OnApplicationPauseObservable => _applicationPauseSubject;

        protected override void Initialize()
        {
            _applicationPauseSubject = new Subject<bool>();
        }

        private void OnApplicationPause(bool pause)
        {
            _applicationPauseSubject.OnNext(pause);
        }

    }
}