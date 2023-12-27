using System;
using Study.Patten.View;
using Study.Patten.Model;

namespace Study.Patten.Presenter
{
    public interface IPresenter
    {
        public void Initialize(IModel model, IView view);

        public void ManipulateModel();

        public void UpdateView();
    }
}