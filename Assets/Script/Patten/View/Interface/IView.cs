using System;
using Study.Patten.Common;

namespace Study.Patten.View
{
    public interface IView
    {
        public IObservable<T> Subject<T>(Delegate @delegate);

        public void UpdateView(IData data);
    }
}