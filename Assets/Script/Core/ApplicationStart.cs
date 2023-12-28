using UnityEngine;
using Study.Scene.Presenter;
using Study.Scene.View;
using Study.Scene.Model;
using Study.Scene.Util;
using Cysharp.Threading.Tasks;

namespace Study.Scene
{
    public class ApplicationStart : MonoBehaviour
    {
        [Header("Model")]
        private SceneLoadingModel _model;

        [Header("Presenter")]
        private ScenePresenter _presenter;

        [Header("View")]
        [SerializeField] private SceneLoadingView _view;

        private SceneLoadAsync _sceneLoadAsync;
        private const string Main = "MainMenu";

        private void Awake()
        {
            _sceneLoadAsync = new SceneLoadAsync(new SceneLoader());
            _presenter = new ScenePresenter(_sceneLoadAsync);
            _model = new SceneLoadingModel(new SceneLoadingData());
            _view = _view ?? GetComponent<SceneLoadingView>();
        }

        private void Start()
        {
            _presenter.Initialize(_model, _view);
            OnApplicationStart().Forget();
        }

        private async UniTaskVoid OnApplicationStart()
        {
            await _sceneLoadAsync.LoadSceneAsync(Main, UnityEngine.SceneManagement.LoadSceneMode.Additive);

            Destroy(gameObject);
        }
    }
}