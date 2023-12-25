using UnityEngine;
using Study.Common.Core;
using Cysharp.Threading.Tasks;

namespace Study.Test
{
    public class TestBasePopup : BasePopup
    {
        [SerializeField] private Animator _animator;

        private AnimatorAsync _animatorAsync;

        private readonly string ShowTrigger = "Show";
        private readonly string HideTrigger = "Hide";

        private void Awake()
        {
            _animatorAsync = new AnimatorAsync(_animator);
        }

        private void OnEnable()
        {
            _animatorAsync.CreateToken();
            _animatorAsync.SetTriggerAsync(ShowTrigger).Forget();
        }

        protected async override void OnHidden()
        {
            await _animatorAsync.SetTriggerAsync(HideTrigger);
            _animatorAsync.Dispose();
            base.OnHidden();
        }
    }
}