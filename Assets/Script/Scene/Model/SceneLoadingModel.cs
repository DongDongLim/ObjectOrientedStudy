using System;
using Study.Patten.Generic;
using Study.Patten.Model;
using UniRx;

namespace Study.Scene.Model
{
    public class SceneLoadingModel : IModel
    {
        private  ReactiveProperty<IData> _data;

        public SceneLoadingModel(IData data)
        {
            _data = new ReactiveProperty<IData>(data);
        }

        public IData GetData<T>() where T : IData
        {
            if(_data.Value is T == false)
                throw new InvalidCastException($"Wrong Type");
            return _data.Value;
        }

        public IDisposable Subject<T>(Delegate @delegate)
        {
            if (_data.Value is T == false)
                throw new InvalidCastException($"Wrong Type");

            IDisposable disposable = _data.Subscribe(value =>
                {
                    @delegate?.DynamicInvoke();
                });

            return disposable;
        }

        public void UpdateData<T>(IData data) where T : IData
        {
            _data.Value.Update(data);
        }
    }
}