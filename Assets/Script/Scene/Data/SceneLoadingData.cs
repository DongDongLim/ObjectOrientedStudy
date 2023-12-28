using System;
using Study.Patten.Generic;
using System.Collections.Generic;

namespace Study.Scene
{
    public class SceneLoadingData : IData
    {
        public KeyValuePair<eSceneState, string> sceneState { private set; get; }

        public const float fadeInDuration = 0.5f;
        public const float fadeOutDuration = 0.5f;

        public SceneLoadingData()
        {

        }

        public SceneLoadingData(KeyValuePair<eSceneState, string> state)
        {
            sceneState = state;
        }

        public T GetData<T>() where T : class, IData
        {
            var result = this as T;
            if (result == null)
                throw new InvalidCastException($"Wrong Type, This Type {typeof(SceneLoadingData)}");
            return result;
        }

        public void Update(IData data)
        {
            var value = data.GetData<SceneLoadingData>();
            sceneState = value.sceneState;
        }
    }
}