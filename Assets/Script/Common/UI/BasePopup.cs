using UnityEngine;

namespace Study.Core
{
    public abstract class BasePopup : MonoBehaviour, IShowableHideable
    {
        protected enum ePopupState
        {
            Showing,
            Show,
            Hiding,
            Hide
        }

        protected ePopupState State { private set; get; }

        public void Show()
        {
            if (ePopupState.Showing == State)
                return;
            State = ePopupState.Showing;
            OnShown();
            State = ePopupState.Show;
        }

        public void Hide()
        {
            if (ePopupState.Hiding == State)
                return;
            State = ePopupState.Hiding;
            OnHidden();
            State = ePopupState.Hide;
        }

        protected virtual void OnShown()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnHidden()
        {
            gameObject.SetActive(false);
        }
    }
}
