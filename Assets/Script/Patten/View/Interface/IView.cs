using System;
using Study.Patten.Generic;

namespace Study.Patten.View
{
    public interface IView
    {
        public IDisposable Subject<T>(Delegate @delegate);

        public void UpdateView(IData data);
    }
}