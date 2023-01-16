using Template.Scripts.Core;

namespace Template.Scripts.Pool
{
    public abstract class CorePoolElement : BaseBehaviour
    {
        public CorePoolElement Prefab { get; set; }

        public virtual void OnPoolSpawn(){ }
        public virtual void OnPoolGet() => gameObject.SetActive(true);
        public virtual void OnPoolReturn() => gameObject.SetActive(false);
    }
}
