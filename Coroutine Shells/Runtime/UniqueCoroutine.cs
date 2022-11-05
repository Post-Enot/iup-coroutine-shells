using System;
using System.Collections;
using UnityEngine;

namespace IUP_Toolkits.CoroutineShells
{
    public sealed class UniqueCoroutine
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        public bool IsPerformed { get; private set; }
        public readonly MonoBehaviour Performer;

        private readonly Func<IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        public void Start()
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine());
            }
        }

        public void StartAnyway()
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine());
        }

        public void Stop()
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
                IsPerformed = false;
            }
        }

        private IEnumerator Routine()
        {
            IsPerformed = true;
            yield return _getRoutine();
            IsPerformed = false;
        }
    }
}
