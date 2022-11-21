using System;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    public sealed class UniqueCoroutine<T1, T2, T3>
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<T1, T2, T3, IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        public bool IsPerformed { get; private set; }
        public readonly MonoBehaviour Performer;

        private readonly Func<T1, T2, T3, IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        public void Start(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine(arg1, arg2, arg3));
            }
        }

        public void StartAnyway(T1 arg1, T2 arg2, T3 arg3)
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(arg1, arg2, arg3));
        }

        public void Stop()
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
                IsPerformed = false;
            }
        }

        private IEnumerator Routine(T1 arg1, T2 arg2, T3 arg3)
        {
            IsPerformed = true;
            yield return _getRoutine(arg1, arg2, arg3);
            IsPerformed = false;
        }
    }
}
