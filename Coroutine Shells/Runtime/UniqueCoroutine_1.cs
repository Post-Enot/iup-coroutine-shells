using System;
using System.Collections;
using UnityEngine;

namespace IUP_Toolkits.CoroutineShells
{
    public sealed class UniqueCoroutine<T>
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<T, IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        public bool IsPerformed { get; private set; }
        public readonly MonoBehaviour Performer;

        private readonly Func<T, IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        public void Start(T arg)
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine(arg));
            }
        }

        public void StartAnyway(T arg)
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(arg));
        }

        public void Stop()
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
                IsPerformed = false;
            }
        }

        private IEnumerator Routine(T arg)
        {
            IsPerformed = true;
            yield return _getRoutine(arg);
            IsPerformed = false;
        }
    }
}
