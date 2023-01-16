using Template.Scripts.Core;

namespace Template.Scripts.General
{
    public class View : BaseBehaviour
    {
        public bool IsActive { get; private set; }
        protected override void OnAwake() => IsActive = gameObject.activeSelf;

        public void SetActive(bool active)
        {
            if (active) Enable();
            else Disable();
        }

        public void Enable()
        {
            if (IsActive) return;
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (!IsActive) return;
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}
