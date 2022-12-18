using System;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    public abstract class BaseCoroutineShell
    {
        /// <summary>
        /// True, если корутина запущена и MonoBehaviour-скрипт, выполняющий её, включён и активен.
        /// </summary>
        public bool IsPerformed => RoutineShell.IsPerformed;
        /// <summary>
        /// True, если корутина запущена.
        /// </summary>
        public bool IsStarted => RoutineShell.IsStarted;
        /// <summary>
        /// MonoBehaviour-скрипт, выполняющий корутину.
        /// </summary>
        public MonoBehaviour Performer => RoutineShell.Performer;

        protected RoutineShell RoutineShell { get; } = new();

        /// <summary>
        /// Инициализирует MonoBehaviour-скрипт, выполняющий корутину. Если в момент инициализации 
        /// корутина уже запущена, будет вызвано исключение.
        /// </summary>
        /// <param name="performer">MonoBehaviour-скрипт, выполняющий корутину.</param>
        public void SetPerformer(MonoBehaviour performer)
        {
            RoutineShell.SetPerformer(performer);
        }

        /// <summary>
        /// Останавливает выполнение корутины.
        /// </summary>
        public void Stop()
        {
            RoutineShell.Stop();
        }
    }
}
