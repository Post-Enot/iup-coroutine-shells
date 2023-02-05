using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Абстрактный класс-основа для оболочек корутин, предоставляющий основные методы и свойства для 
    /// работы с корутинами.
    /// </summary>
    public abstract class BaseCoroutineShell : IRoutineShell
    {
        public bool IsPerformed => RoutineShell.IsPerformed;
        public bool IsStarted => RoutineShell.IsStarted;
        public MonoBehaviour Performer => RoutineShell.Performer;

        protected RoutineShell RoutineShell { get; } = new();

        public void SetPerformer(MonoBehaviour performer)
        {
            RoutineShell.SetPerformer(performer);
        }

        public void Stop()
        {
            RoutineShell.Stop();
        }
    }
}
