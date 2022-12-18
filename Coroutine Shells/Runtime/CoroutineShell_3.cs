using System;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Оболочка для удобной работы с Unity-корутинами.
    /// </summary>
    public sealed class CoroutineShell<T1, T2, T3> : BaseCoroutineShell
    {
        public CoroutineShell() { }

        /// <summary>
        /// Инициализирует оболочку для работы с Unity-корутинами.
        /// </summary>
        /// <param name="performer">MonoBehaviour-скрипт, выполяющий корутину.</param>
        /// <param name="routine">Корутина.</param>
        public CoroutineShell(MonoBehaviour performer, Func<T1, T2, T3, IEnumerator> routine)
        {
            RoutineShell.SetPerformer(performer);
            _routine = routine;
        }

        private Func<T1, T2, T3, IEnumerator> _routine;

        /// <summary>
        /// Инициализирует функцию корутины. Если в момент инициализации корутина уже запущена, 
        /// будет вызвано исключение.
        /// </summary>
        /// <param name="routine">Корутина.</param>
        public void SetCoroutine(Func<T1, T2, T3, IEnumerator> routine)
        {
            if (IsStarted)
            {
                throw new InvalidOperationException(
                    "Невозможно изменить MonoBehaviour-скрипт, выполняющий корутину, когда " +
                    "корутина уже запущена.");
            }
            _routine = routine;
        }

        /// <summary>
        /// Запускает выполнение корутины. В случае, если выполнение уже запущено, будет вызвано 
        /// исключение.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start(T1 arg1, T2 arg2, T3 arg3)
        {
            RoutineShell.Start(_routine(arg1, arg2, arg3));
        }

        /// <summary>
        /// Запускает выполнение корутины; в случае, если выполнение уже запущено, оно будет остановлено 
        /// и запущено вновь.
        /// </summary>
        public void StartAnyway(T1 arg1, T2 arg2, T3 arg3)
        {
            RoutineShell.StartAnyway(_routine(arg1, arg2, arg3));
        }
    }
}
