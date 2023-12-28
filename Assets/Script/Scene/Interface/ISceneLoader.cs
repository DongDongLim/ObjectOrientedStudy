using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Study.Scene
{
    public interface ISceneLoader
    {
        public bool IsLoadingScene(string sceneName);

        public bool IsUnLoadingScene(string sceneName);

        public UniTask LoadSceneAsync(string sceneName, LoadSceneMode sceneMode);

        public UniTask UnLoadSceneAsync(string sceneName);
    }
}