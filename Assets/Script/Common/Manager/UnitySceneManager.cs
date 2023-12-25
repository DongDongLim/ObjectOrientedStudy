using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Study.Common.Enum;
using UniRx;

namespace Study.Common.Core
{
    public class UnitySceneManager : Singleton<UnitySceneManager>
    {
        private Subject<KeyValuePair<eSceneState, string>> _sceneLoadSubject;
        private Subject<KeyValuePair<eSceneState, string>> _sceneUnLoadSubject;

        public IObservable<KeyValuePair<eSceneState, string>> SceneLoadObservable => _sceneLoadSubject;
        public IObservable<KeyValuePair<eSceneState, string>> SceneUnLoadObservable => _sceneUnLoadSubject;

        private ISceneLoader _sceneLoader;

        public UnitySceneManager()
        {
            _sceneLoadSubject = _sceneLoadSubject ?? new Subject<KeyValuePair<eSceneState, string>>();
            _sceneUnLoadSubject = _sceneUnLoadSubject ?? new Subject<KeyValuePair<eSceneState, string>>();
            _sceneLoader = _sceneLoader ?? new SceneLoader();
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Additive)
        {
            if (true == _sceneLoader.IsLoadingScene(sceneName))
                return;

            _sceneLoadSubject.OnNext(new KeyValuePair<eSceneState, string>(eSceneState.LoadingStart, sceneName));
            await _sceneLoader.LoadSceneAsync(sceneName, sceneMode);
            _sceneLoadSubject.OnNext(new KeyValuePair<eSceneState, string>(eSceneState.LoadingComplate, sceneName));
        }

        public async UniTask UnLoadSceneAsync(string sceneName)
        {
            if (true == _sceneLoader.IsUnLoadingScene(sceneName))
                return;

            _sceneUnLoadSubject.OnNext(new KeyValuePair<eSceneState, string>(eSceneState.LoadingStart, sceneName));
            await SceneManager.UnloadSceneAsync(sceneName);
            _sceneUnLoadSubject.OnNext(new KeyValuePair<eSceneState, string>(eSceneState.LoadingComplate, sceneName));
        }
    }

    internal class SceneLoader : ISceneLoader
    {
        // 씬 중복 로딩 방지
        public static LinkedList<string> LoadingSceneNames = new LinkedList<string>();
        public static LinkedList<string> UnLoadingSceneNames = new LinkedList<string>();

        public bool IsLoadingScene(string sceneName)
        {
            return LoadingSceneNames.Contains(sceneName);
        }

        public bool IsUnLoadingScene(string sceneName)
        {
            return UnLoadingSceneNames.Contains(sceneName);
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode sceneMode)
        {
            LoadingSceneNames.AddLast(sceneName);

            await SceneManager.LoadSceneAsync(sceneName, sceneMode);

            LoadingSceneNames.Remove(sceneName);
        }

        public async UniTask UnLoadSceneAsync(string sceneName)
        {
            UnLoadingSceneNames.AddLast(sceneName);

            await SceneManager.UnloadSceneAsync(sceneName);

            UnLoadingSceneNames.Remove(sceneName);
        }
    }
}