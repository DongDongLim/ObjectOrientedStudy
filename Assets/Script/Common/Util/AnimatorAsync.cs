using System;
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

        private CancellationTokenSource _animatorTaskWithMonoBehaviourCancellationTokenSource;

        #region MonoBehaviour

        private void OnEnable()
        {
            _animatorTaskWithMonoBehaviourCancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _animatorTaskWithMonoBehaviourCancellationTokenSource?.Cancel();
            _animatorTaskWithMonoBehaviourCancellationTokenSource?.Dispose();
            _animatorTaskWithMonoBehaviourCancellationTokenSource = null;
        }
        #endregion

        public async UniTask SetTriggerAsync(string trigger)
        {
            if (_animator == null || _animator.isActiveAndEnabled == false)
                return;

            if (_animatorTaskWithMonoBehaviourCancellationTokenSource == null)
                throw new NullReferenceException("Please activate the game object in AnimatorAsync or provide a CancellationToken.");

            await SetTriggerAsync(trigger, _animatorTaskWithMonoBehaviourCancellationTokenSource);
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

            if (_animatorTaskWithMonoBehaviourCancellationTokenSource == null)
                throw new NullReferenceException("Please activate the game object in AnimatorAsync or provide a CancellationToken.");

            await SetTriggerAsync(id, _animatorTaskWithMonoBehaviourCancellationTokenSource);
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