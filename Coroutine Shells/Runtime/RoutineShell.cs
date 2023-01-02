using System;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.CoroutineShells
{
    /// <summary>
    /// Оболочка для удобной работы с Unity-корутинами, предоставляющая свойства и методы для запуска, 
    /// остановки и проверки состояния выполнения корутины.
    /// </summary>
    public sealed class RoutineShell : IRoutineShell
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public RoutineShell() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса, назначая объект класса MonoBehaiour, выплоняющий 
        /// корутину.
        /// </summary>
        /// <param name="performer">Объект класса MonoBehaviour, выполняющий корутину.</param>
        public RoutineShell(MonoBehaviour performer)
        {
            Performer = performer;
        }

        public bool IsPerformed => IsStarted && Performer.isActiveAndEnabled;
        public bool IsStarted
        {
            get
            {
                if (_isStarted)
                {
                    return Performer != null;
                }
                return false;
            }
        }
        public MonoBehaviour Performer { get; private set; }

        private Coroutine _coroutine;
        private bool _isStarted;

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
        /// <param name="routine">Интерфейс перечислителя корутины.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start(IEnumerator routine)
        {
            if (Performer == null)
            {
                throw NewPerformerNullReferenceException();
            }
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
        /// и запущено вновь. В этом случае вызов метода будет равносилен перезапуску выполнения корутины.
        /// </summary>
        /// <param name="routine">Интерфейс перечислителя корутины.</param>
        public void StartAnyway(IEnumerator routine)
        {
            if (Performer == null)
            {
                throw NewPerformerNullReferenceException();
            }
            if (IsStarted)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(routine));
        }

        public void Stop()
        {
            if (Performer == null)
            {
                throw NewPerformerNullReferenceException();
            }
            if (IsStarted)
            {
                Performer.StopCoroutine(_coroutine);
                _isStarted = false;
            }
        }

        private IEnumerator Routine(IEnumerator routine)
        {
            _isStarted = true;
            yield return routine;
            _isStarted = false;
        }

        private NullReferenceException NewPerformerNullReferenceException()
        {
            return new NullReferenceException(
                "Объект класса MonoBehaviour, выполняющий корутину, не был назначен или был удалён.");
        }
    }
}
