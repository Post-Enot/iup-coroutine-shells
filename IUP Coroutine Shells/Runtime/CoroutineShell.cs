using System;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Оболочка корутины, связываемая с методом корутины, не принимающим аргументы.
    /// </summary>
    public sealed class CoroutineShell : BaseCoroutineShell
    {
        public CoroutineShell() { }

        /// <summary>
        /// Инициализирует оболочку для работы с Unity-корутинами.
        /// </summary>
        /// <param name="performer">MonoBehaviour-скрипт, выполяющий корутину.</param>
        /// <param name="routine">Корутина.</param>
        public CoroutineShell(MonoBehaviour performer, Func<IEnumerator> routine)
        {
            RoutineShell.SetPerformer(performer);
            _routine = routine;
        }

        private Func<IEnumerator> _routine;

        /// <summary>
        /// Инициализирует функцию корутины. Если в момент инициализации корутина уже запущена, 
        /// будет вызвано исключение.
        /// </summary>
        /// <param name="routine">Корутина.</param>
        public void SetCoroutine(Func<IEnumerator> routine)
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
        public void Start()
        {
            RoutineShell.Start(_routine());
        }

        /// <summary>
        /// Запускает выполнение корутины; в случае, если выполнение уже запущено, оно будет остановлено 
        /// и запущено вновь.
        /// </summary>
        public void StartAnyway()
        {
            RoutineShell.StartAnyway(_routine());
        }
    }
}
