using UnityEngine;

namespace Study.Core
{
    [RequireComponent(typeof(AnimatorAsync))]
    public abstract class BasePopup : MonoBehaviour, IShowableHideable
    {
        public void Show()
        {
            OnShown();
        }

        public void Hide()
        {
            OnHiden();
        }

        protected virtual void OnShown()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnHiden()
        {
            gameObject.SetActive(false);
        }
    }
}
