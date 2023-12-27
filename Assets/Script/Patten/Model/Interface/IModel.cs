using Study.Patten.Common;
using System;

namespace Study.Patten.Model
{
    public interface IModel
    {
        public IObservable<T> Subject<T>(Delegate @delegate);

        public void UpdateData<T>(IData data) where T : IData;

        public IData GetData<T>() where T : IData;
    }
}