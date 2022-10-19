using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    /// <summary>
    /// Класс-оболочка, инкапсулирующая логику взаимодействия с корутинами, принимающими 3 аргумента.
    /// </summary>
    public sealed class UniqueCoroutine<T1, T2, T3>
    {
        /// <summary>
        /// Создаёт объект-оболочку, готовую к использованию.
        /// </summary>
        /// <param name="performer">MonoBehaviour-объект, с помощью которого будет запускаться корутина.
        /// Отключение или удаление этого объекта приведёт к остановке действия корутины.</param>
        /// <param name="getRoutine">Делегат-метод корутины.</param>
        public UniqueCoroutine(MonoBehaviour performer, Func<T1, T2, T3, IEnumerator> getRoutine)
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

        private readonly Func<T1, T2, T3, IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        /// <summary>
        /// Запускает корутину, если она до этого не была запущена. В противном случае операция игнорируется.
        /// </summary>
        /// <param name="arg1">Первый аргумент, принимаемый корутиной.</param>
        /// <param name="arg2">Второй аргумент, принимаемый корутиной.</param>
        /// <param name="arg3">Третий аргумент, принимаемый корутиной.</param>
        public void Start(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!IsPerformed)
            {
                _coroutine = Performer.StartCoroutine(Routine(arg1, arg2, arg3));
            }
        }

        /// <summary>
        /// Запускает корутину.
        /// Если она до этого была запущена, то принудительно завершает её выполнение, после чего запускает вновь.
        /// </summary>
        /// <param name="arg1">Первый аргумент, принимаемый корутиной.</param>
        /// <param name="arg2">Второй аргумент, принимаемый корутиной.</param>
        /// <param name="arg3">Третий аргумент, принимаемый корутиной.</param>
        public void StartAnyway(T1 arg1, T2 arg2, T3 arg3)
        {
            if (IsPerformed)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine(arg1, arg2, arg3));
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

        private IEnumerator Routine(T1 arg1, T2 arg2, T3 arg3)
        {
            IsPerformed = true;
            yield return _getRoutine(arg1, arg2, arg3);
            IsPerformed = false;
        }
    }
}
