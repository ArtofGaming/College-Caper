using System;
using System.Collections;

namespace Naninovel
{
    // UnityEngine Bridges.

    public readonly partial struct UniTask
    {
        public static IEnumerator ToCoroutine (Func<UniTask> taskFactory)
        {
            return taskFactory().ToCoroutine();
        }
    }
}
