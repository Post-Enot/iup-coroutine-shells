using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    /// <summary>
    /// Класс-оболочка, инкапсулирующая логику взаимодействия с корутинами, принимающими 1 аргумент.
    /// </summary>
    public sealed class UniqueCoroutine<T>
    {
        /// <summary>
        /// Создаёт объект-оболочку, готовую к использованию.
        /// </summary>
        /// <param name="performer">MonoBehaviour-объект, с помощью которого будет запускаться корутина.
        /// Отключение или удаление этого объекта приведёт к остановке действия корутины.</param>
        /// <param name="getRoutine">Делегат-метод корутины.</param>
        public UniqueCoroutine(MonoBehaviour performer, Func<T, IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        /// <summary>
        /// Запущена ли корутина в текущий момент.
        /// </summary>
        public bool IsPerformed { get; private set; }
        /// <summary>
        /// Ссылка на объект, с помощью которого запускается корутина.
        /// </summary>
        public readonly MonoBehaviour Performer;

        private readonly Func<T, IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        /// <summary>
        /// Запускает корутину, если она до этого не была запущена. В противном случае операция игнорируется.
        /// </summary>
        /// <param name="arg">Аргумент, принимаемый корутиной.</param>
        public void Start(T arg)
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine(arg));
            }
        }

        /// <summary>
        /// Запускает корутину.
        /// Если она до этого была запущена, то принудительно завершает её выполнение, после чего запускает вновь.
        /// </summary>
        /// <param name="arg">Аргумент, принимаемый корутиной.</param>
        public void StartAnyway(T arg)
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(arg));
        }

        /// <summary>
        /// Останавливает корутину, если она до этого была запущена. В противном случае операция игнорируется.
        /// </summary>
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
