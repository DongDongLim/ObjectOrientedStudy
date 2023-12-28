using System;
using UnityEngine;
using UnityEngine.UI;
using Study.Patten.Generic;
using Study.Patten.View;
using Cysharp.Threading.Tasks;

namespace Study.Scene.View
{
    public class SceneLoadingView : MonoBehaviour, IView, IShowableHideable
    {
        [SerializeField] private Image _backGroundPanel;

        private bool _isFading = false;

        private void Awake()
        {
            _backGroundPanel = GetComponent<Image>();
        }

        public async void Hide()
        {
            await FadeChange(false);
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            FadeChange(true).Forget();
        }

        private async UniTask FadeChange(bool isFaidIn)
        {
            await UniTask.WaitUntil(() => _isFading == false);

            _isFading = true;

            float startAlpha = isFaidIn == true ? 0 : 1;
            float endAlpha = isFaidIn == true ? 1 : 0;
            float duration = isFaidIn == true ? SceneLoadingData.fadeInDuration : SceneLoadingData.fadeOutDuration;

            _backGroundPanel.color = new Color(_backGroundPanel.color.r, _backGroundPanel.color.b, _backGroundPanel.color.g, startAlpha);
            _backGroundPanel.CrossFadeAlpha(endAlpha, duration, true);

            await UniTask.Delay(TimeSpan.FromSeconds(duration), true);

            _isFading = false;
        }

        public IDisposable Subject<T>(Delegate @delegate)
        {
            throw new NotImplementedException();
        }

        public void UpdateView(IData data)
        {
            SceneLoadingData loadingData = data.GetData<SceneLoadingData>();

            switch(loadingData.sceneState.Key)
            {
                case eSceneState.LoadingStart:
                    {
                        Show();
                        break;
                    }
                case eSceneState.LoadingComplate:
                    {
                        Hide();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}