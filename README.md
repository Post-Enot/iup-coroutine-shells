Unity-пакет, содержащий классы-оболочки для инкапсуляции логики работы Unity-корутин.

## Как устроены

Пакет содержит следующие классы-оболочки Unity-корутин:

**[IUP.Toolkits.CoroutineShells.RoutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/RoutineShell.cs#L10)** - 
инкапсулирует логику работы с корутинами, позволяя назначать MonoBehaviour-скрипт, исполняющий корутину, отслеживать статус запуска, исполнения, а также запускать и 
останавливать выполнение корутины. Метод запуска корутины в качестве аргумента принимает IEnumerator корутины, подобно тому, как это происходит со стандартным вызовом 
корутины с помощью метода **UnityEngine.MonoBenaviourStartCoroutine(IEnumerator routine)**.

**[IUP.Toolkits.CoroutineShells.CoroutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/CoroutineShell.cs#L10)** - 
инкапсулирует логику работы с методами корутин без аргументов.
