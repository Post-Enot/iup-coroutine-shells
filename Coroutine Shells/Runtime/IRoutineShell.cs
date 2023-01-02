using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Интерфейс для оболочек корутин, предоставляющий базовые методы и свойства для взаимодействия с 
    /// корутинами.
    /// </summary>
    public interface IRoutineShell
    {
        /// <summary>
        /// True, если корутина запущена и объект класса MonoBehaviour, выполняющий её, включён и активен.
        /// </summary>
        public bool IsPerformed { get; }

        /// <summary>
        /// True, если корутина запущена; если объект класса MonoBehaviour, выполняющий её, был удалён после 
        /// запуска, вернёт False.
        /// </summary>
        public bool IsStarted { get; }
        /// <summary>
        /// Объект класса MonoBehaviour, выполняющий корутину.
        /// </summary>
        public MonoBehaviour Performer { get; }

        /// <summary>
        /// Назначает объект класса MonoBehaviour, выполняющий корутину. Если в момент инициализации 
        /// корутина уже запущена, будет вызвано исключение.
        /// </summary>
        /// <param name="performer">Объект класса MonoBehaviour, выполняющий корутину.</param>
        public void SetPerformer(MonoBehaviour performer);

        /// <summary>
        /// Останавливает выполнение корутины.
        /// </summary>
        public void Stop();
    }
}
