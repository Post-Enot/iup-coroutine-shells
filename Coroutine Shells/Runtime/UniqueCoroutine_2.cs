using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    public sealed class UniqueCoroutine<T1, T2>
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<T1, T2, IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        public bool IsPerformed { get; private set; }
        public readonly MonoBehaviour Performer;

        private readonly Func<T1, T2, IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        public void Start(T1 arg1, T2 arg2)
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine(arg1, arg2));
            }
        }

        public void StartAnyway(T1 arg1, T2 arg2)
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(arg1, arg2));
        }

        public void Stop()
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
                IsPerformed = false;
            }
        }

        private IEnumerator Routine(T1 arg1, T2 arg2)
        {
            IsPerformed = true;
            yield return _getRoutine(arg1, arg2);
            IsPerformed = false;
        }
    }
}
