using UnityEngine;
using Study.Core;
using Cysharp.Threading.Tasks;

namespace Study.Test
{
    [RequireComponent(typeof(AnimatorAsync))]
    public class TestBasePopup : BasePopup
    {
        [SerializeField] private AnimatorAsync _animator;

        private readonly string ShowTrigger = "Show";
        private readonly string HideTrigger = "Hide";

        private void Awake()
        {
            _animator = GetComponent<AnimatorAsync>();
        }

        private void OnEnable()
        {
            _animator.SetTriggerAsync(ShowTrigger).Forget();
        }

        protected async override void OnHiden()
        {
            await _animator.SetTriggerAsync(HideTrigger);
            base.OnHiden();
        }
    }
}