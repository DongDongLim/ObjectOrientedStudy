using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Study.Core
{
    public class AnimatorAsync : IDisposable
    {
        private readonly Animator _animator;

        private CancellationTokenSource _cancellationTokenSource;

        private AnimatorAsync() { }

        public AnimatorAsync(Animator animator)
        {
            if (null == animator)
                throw new ArgumentNullException(nameof(Animator), "animator is null");
            _animator = animator;
        }

        public void CreateToken()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void CancelToken()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        public async UniTask SetTriggerAsync(string trigger)
        {
            if (_animator == null || _animator.isActiveAndEnabled == false)
                return;

            await SetTriggerAsync(trigger, _cancellationTokenSource);
        }

        public async UniTask SetTriggerAsync(string trigger, CancellationTokenSource cancellationToken)
        {
            if (_animator == null || _animator.isActiveAndEnabled == false)
                return;

            CancellationTokenSource tokenSource = cancellationToken;

            int triggerHash = Animator.StringToHash(trigger);
            await SetTriggerAsync(triggerHash, tokenSource);
        }

        public async UniTask SetTriggerAsync(int id)
        {
            if (_animator == null || _animator.isActiveAndEnabled == false)
                return;

            await SetTriggerAsync(id, _cancellationTokenSource);
        }

        public async UniTask SetTriggerAsync(int id, CancellationTokenSource cancellationToken)
        {
            if (_animator == null || _animator.isActiveAndEnabled == false)
                return;

            AnimatorStateInfo prevStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            _animator.SetTrigger(id);
            AnimatorStateInfo stateInfo = default;

            await UniTask.WaitUntil(() =>
            {
                stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                return prevStateInfo.fullPathHash != stateInfo.fullPathHash;
            }, cancellationToken: cancellationToken.Token);

            await UniTask.WaitUntil(() =>
            {
                stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.normalizedTime >= 1;
            }, cancellationToken: cancellationToken.Token);
        }


    }
}