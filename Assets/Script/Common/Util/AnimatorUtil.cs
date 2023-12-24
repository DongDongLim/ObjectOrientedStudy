using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Study.Core
{
    [AddComponentMenu("Study/Animator")]
    public class AnimatorAsync : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Animator Anim => _animator;

        private CancellationTokenSource _animatorTaskCancellationTokenSource;

        private void OnEnable()
        {
            _animatorTaskCancellationTokenSource = new CancellationTokenSource();
        }

        public async UniTask SetTriggerAsync(string trigger)
        {
            int triggerHash = Animator.StringToHash(trigger);
            await SetTriggerAsync(triggerHash);
        }

        public async UniTask SetTriggerAsync(int id)
        {
            AnimatorStateInfo prevStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            _animator.SetTrigger(id);
            AnimatorStateInfo stateInfo = default;

            await UniTask.WaitUntil(() =>
            {
                stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                return prevStateInfo.fullPathHash != stateInfo.fullPathHash;
            }, cancellationToken: _animatorTaskCancellationTokenSource.Token);

            await UniTask.WaitUntil(() =>
            {
                stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.normalizedTime >= 1;
            }, cancellationToken: _animatorTaskCancellationTokenSource.Token);
        }

        private void OnDisable()
        {
            _animatorTaskCancellationTokenSource?.Cancel();
            _animatorTaskCancellationTokenSource?.Dispose();
            _animatorTaskCancellationTokenSource = null;
        }
    }
}