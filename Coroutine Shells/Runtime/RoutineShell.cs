using System;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Оболочка для удобной работы с Unity-корутинами.
    /// </summary>
    public sealed class RoutineShell
    {
        public RoutineShell() { }

        public RoutineShell(MonoBehaviour performer)
        {
            Performer = performer;
        }

        /// <summary>
        /// True, если корутина запущена и MonoBehaviour-скрипт, выполняющий её, включён и активен.
        /// </summary>
        public bool IsPerformed => IsStarted && Performer.isActiveAndEnabled;
        /// <summary>
        /// True, если корутина запущена.
        /// </summary>
        public bool IsStarted { get; private set; }
        /// <summary>
        /// MonoBehaviour-скрипт, выполняющий корутину.
        /// </summary>
        public MonoBehaviour Performer { get; private set; }

        private Coroutine _coroutine;

        /// <summary>
        /// Инициализирует MonoBehaviour-скрипт, выполняющий корутину. Если в момент инициализации 
        /// корутина уже запущена, будет вызвано исключение.
        /// </summary>
        /// <param name="performer">MonoBehaviour-скрипт, выполняющий корутину.</param>
        public void SetPerformer(MonoBehaviour performer)
        {
            if (IsStarted)
            {
                throw new InvalidOperationException(
                    "Невозможно изменить MonoBehaviour-скрипт, выполняющий корутину, когда " +
                    "корутина уже запущена.");
            }
            Performer = performer;
        }

        /// <summary>
        /// Запускает выполнение корутины. В случае, если выполнение уже запущено, будет вызвано 
        /// исключение.
        /// </summary>
        /// <param name="routine">IEnumerator корутины.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start(IEnumerator routine)
        {
            if (IsStarted)
            {
                throw new InvalidOperationException("Попытка повторного запуска корутины: в случае, " +
                    "если вам необходимо остановить уже запущенную корутину и запустить её вновь, " +
                    $"используйте метод {nameof(StartAnyway)}");
            }
            _coroutine = Performer.StartCoroutine(Routine(routine));
        }

        /// <summary>
        /// Запускает выполнение корутины; в случае, если выполнение уже запущено, оно будет остановлено 
        /// и запущено вновь.
        /// </summary>
        /// <param name="routine">IEnumerator корутины.</param>
        public void StartAnyway(IEnumerator routine)
        {
            if (IsStarted)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(routine));
        }

        /// <summary>
        /// Останавливает выполнение корутины.
        /// </summary>
        public void Stop()
        {
            if (IsStarted)
            {
                Performer.StopCoroutine(_coroutine);
                IsStarted = false;
            }
        }

        private IEnumerator Routine(IEnumerator routine)
        {
            IsStarted = true;
            yield return routine;
            IsStarted = false;
        }
    }
}
