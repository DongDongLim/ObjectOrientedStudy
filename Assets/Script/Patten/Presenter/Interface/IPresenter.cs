using Study.Patten.Generic;
using Study.Patten.Model;
using Study.Patten.View;

namespace Study.Patten.Presenter
{
    public interface IPresenter
    {
        public void Initialize(IModel model, IView view);

        public void ManipulateModel(IData data);

        public void UpdateView();
    }
}