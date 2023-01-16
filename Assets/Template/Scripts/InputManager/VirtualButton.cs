using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Project.Scripts.InputSystem
{
    public class VirtualButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Select The Control Path Of The Button")]
        [InputControl(layout = "Float")]
        [SerializeField] private string _controlPath;
        public bool IsDown { get; private set; }
        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsDown = false;
        }

        private void Update()
        {
            SendValueToControl(IsDown ? 1f : 0f);
        }
    }
}