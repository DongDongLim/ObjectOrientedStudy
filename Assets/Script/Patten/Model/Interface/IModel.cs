using Study.Patten.Generic;
using System;

namespace Study.Patten.Model
{
    public interface IModel
    {
        public IDisposable Subject<T>(Delegate @delegate);

        public void UpdateData<T>(IData data) where T : IData;

        public IData GetData<T>() where T : IData;
    }
}