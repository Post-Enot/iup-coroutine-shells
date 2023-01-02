Unity-пакет, содержащий классы-оболочки для инкапсуляции логики работы Unity-корутин.

# Виды оболочек

Пакет содержит следующие классы-оболочки Unity-корутин:

**[IUP.Toolkits.CoroutineShells.RoutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/RoutineShell.cs#L10)** -
инкапсулирует логику работы с корутинами, позволяя назначать MonoBehaviour-скрипт, исполняющий корутину, отслеживать статус запуска, исполнения, а также запускать и
останавливать выполнение корутины. Метод запуска корутины в качестве аргумента принимает IEnumerator корутины, подобно тому, как это происходит со стандартным вызовом
корутины с помощью метода **UnityEngine.MonoBenaviour.StartCoroutine(IEnumerator routine)**.

**[IUP.Toolkits.CoroutineShells.CoroutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/CoroutineShell.cs#L10)** -
инкапсулирует логику работы с методами корутин без аргументов. Используйте, когда хотите связать оболочку корутины с конкретным методом.

Для того, чтобы связать оболочку корутины с методом, принимающим аргументы, используйте обобщённые классы **[IUP.Toolkits.CoroutineShells.CoroutineShell\<T>](https://github.com/Post-Enot/coroutine-shells/blob/05949a34f476018c573a5a2ef695bbfa05e7f312/Coroutine%20Shells/Runtime/CoroutineShell_1.cs#L10)**, **[IUP.Toolkits.CoroutineShells.CoroutineShell<T1, T2>](https://github.com/Post-Enot/coroutine-shells/blob/05949a34f476018c573a5a2ef695bbfa05e7f312/Coroutine%20Shells/Runtime/CoroutineShell_2.cs#L10)** и **[IUP.Toolkits.CoroutineShells.CoroutineShell<T1, T2, T3>](https://github.com/Post-Enot/coroutine-shells/blob/05949a34f476018c573a5a2ef695bbfa05e7f312/Coroutine%20Shells/Runtime/CoroutineShell_3.cs#L10)**.

# Как использовать

Если вам необходимо инкапсулировать саму логику корутин, Вы можете использовать **[IUP.Toolkits.CoroutineShells.RoutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/RoutineShell.cs#L10)**. Её использование наиболее похоже на использование корутин в Unity3D.

**Пример:**

```c#
public class Player : MonoBehaviour
{
    private void Awake()
    {
        RoutineShell routine = new RoutineShell(this);
        routine.Start(Routine());
    }

    private IEnumerator Routine()
    {
        for (int i = 1; i <= 10; i += 1)
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"Timer: {i} sec.");
        }
    }
}
```

Для того, чтобы связать конкретный метод с оболочкой корутины, используйте **[IUP.Toolkits.CoroutineShells.CoroutineShell](https://github.com/Post-Enot/coroutine-shells/blob/main/Coroutine%20Shells/Runtime/CoroutineShell.cs#L10)**.

**Пример:**

```c#
public class Player : MonoBehaviour
{
    private void Awake()
    {
        RoutineShell routine = new RoutineShell(this, Routine);
        routine.Start();
    }

    private IEnumerator Routine()
    {
        for (int i = 1; i <= 10; i += 1)
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"Timer: {i} sec.");
        }
    }
}
```

# Контакты
По всем вопросам писать в Telegram: https://t.me/ProcyonNihil
