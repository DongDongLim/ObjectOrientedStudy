using System;
using Study.Patten.Generic;
using Study.Patten.Model;
using Study.Patten.Presenter;
using Study.Patten.View;
using Study.Scene.Util;
using Study.Scene.View;
using UnityEngine;
using UniRx;

namespace Study.Scene.Presenter
{
    public class ScenePresenter : IPresenter
    {
        [SerializeField] private IModel _loadingModel;

        [SerializeField] private IView _loadingView;

        private SceneLoadAsync _sceneLoadAsync;

        public ScenePresenter(SceneLoadAsync sceneLoadAsync)
        {
            _sceneLoadAsync = sceneLoadAsync;
            _sceneLoadAsync.SceneLoadObservable.Subscribe(keyValueFair =>
            {
                var changeValue = new SceneLoadingData(keyValueFair);
                ManipulateModel(changeValue);
            });
        }

        public void Initialize(IModel model, IView view)
        {
            _loadingModel = model;
            _loadingView = view;
            _loadingModel.Subject<IData>((Action)UpdateView);
        }

        public void ManipulateModel(IData data)
        {
            _loadingModel.UpdateData<IData>(data);
        }

        public void UpdateView()
        {
            _loadingView.UpdateView(_loadingModel.GetData<IData>());
        }
    }
}