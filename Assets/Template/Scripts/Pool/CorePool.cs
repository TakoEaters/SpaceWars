using System.Collections.Generic;
using System.Linq;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Core.Pool
{
    public class CorePool
    {
        private Dictionary<CorePoolElement, Queue<CorePoolElement>> poolElements =
            new Dictionary<CorePoolElement, Queue<CorePoolElement>>();

        public static CorePool Current { get; private set; }

        public static void Initialize()
        {
            Current = new CorePool();
        }

        private Transform _sceneConainer;

        private Transform SceneContainer
        {
            get
            {
                if (_sceneConainer == null)
                {
                    var go = new GameObject
                    {
                        name = "PoolContainer"
                    };
                    _sceneConainer = go.transform;
                }

                return _sceneConainer;
            }
        }

        public T Get<T>(T prefab) where T : CorePoolElement
        {
            if (!poolElements.ContainsKey(prefab)) poolElements[prefab] = new Queue<CorePoolElement>();
            if (poolElements[prefab].Any())
            {
                var element = poolElements[prefab].Dequeue();
                element.OnPoolGet();
                return (T)element;
            }
            else
            {
                var element = (CorePoolElement)Object.Instantiate(prefab);
                element.OnPoolSpawn();
                element.OnPoolGet();
                element.Prefab = prefab;
                return (T)element;
            }
        }

        public T Get<T>(T prefab, Vector3 position, Quaternion rotation) where T : CorePoolElement
        {
            if (!poolElements.ContainsKey(prefab)) poolElements[prefab] = new Queue<CorePoolElement>();
            if (poolElements[prefab].Any())
            {
                var element = poolElements[prefab].Dequeue();
                var elementTransform = element.transform;
                elementTransform.position = position;
                elementTransform.rotation = rotation;
                element.OnPoolGet();
                return (T)element;
            }
            else
            {
                var element = (CorePoolElement)Object.Instantiate(prefab);
                var elementTransform = element.transform;
                elementTransform.position = position;
                elementTransform.rotation = rotation;
                element.OnPoolSpawn();
                element.OnPoolGet();
                element.Prefab = prefab;
                return (T)element;
            }
        }

        public void Return(CorePoolElement element)
        {
            if (!poolElements.ContainsKey(element.Prefab)) poolElements[element.Prefab] = new Queue<CorePoolElement>();
            element.OnPoolReturn();
            element.transform.parent = SceneContainer;
            element.transform.localPosition = Vector3.zero;
            poolElements[element.Prefab].Enqueue(element);
        }
    }
}
