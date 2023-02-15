using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.General.Utils
{
    public static class WaitUtils 
    {
        public static IEnumerator WaitWithDelay(Action action, float duration)
        {
            yield return new WaitForSeconds(duration);
            action?.Invoke();
        }
    }
}
