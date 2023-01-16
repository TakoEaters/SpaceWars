using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Core.Pool
{
    public static class CorePoolUtils
    {
        private static CorePool Pool => CorePool.Current;

        public static T PoolSpawn<T>(this T element) where T : CorePoolElement
        {
            return Pool.Get(element);
        }

        public static T PoolSpawn<T>(this T element, Vector3 position) where T : CorePoolElement
        {
            return Pool.Get(element, position, Quaternion.identity);
        }

        public static T PoolSpawn<T>(this T element, Vector3 position, Quaternion rotation) where T : CorePoolElement
        {
            return Pool.Get(element, position, rotation);
        }

        public static void PoolReturn(this CorePoolElement element)
        {
            Pool.Return(element);
        }
    }
}
