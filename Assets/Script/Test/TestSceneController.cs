using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Study.Common.Core;
using Cysharp.Threading.Tasks;
using UniRx;
using TMPro;

namespace Study.Test
{
    public class TestSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingPanelObject;
        [SerializeField] private TMP_Text _loadSceneName;
        
        private void Awake()
        {
            UnitySceneManager.Instunce.SceneLoadObservable.Where(sceneInfo =>
            {
                return sceneInfo.Key == Common.Enum.eSceneState.LoadingStart;
            }).Subscribe(sceneInfo =>
            {
                _loadSceneName.text = sceneInfo.Value;
                _loadingPanelObject.SetActive(true);
            });

            UnitySceneManager.Instunce.SceneLoadObservable.Where(sceneInfo =>
            {
                return sceneInfo.Key == Common.Enum.eSceneState.LoadingComplate;
            }).Subscribe(sceneInfo =>
            {
                _loadingPanelObject.SetActive(false);
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                UnitySceneManager.Instunce.LoadSceneAsync("Test").Forget();
            }
        }
    }
}