using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Project.Scripts.Core.InputManager
{
    public sealed class VirtualJoystick : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private float Horizontal => Direction.x;
        private float Vertical => Direction.y;

        private Vector2 Direction
        {
            get => _input;
            set
            {
                _input = value;
                OutputVectorValue();
            }
        }

        public Vector2 NormalizedDirection => new Vector2(Horizontal, Vertical).normalized;

        [Header("Select The Control Path Of The Joystick")]
        [InputControl(layout = "Vector2")]
        [SerializeField] private string _controlPath;
        
        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone = 0;
        
        [SerializeField] private RectTransform background = null;
        [SerializeField] private RectTransform handle = null;
        private RectTransform _baseRect = null;

        private Canvas _canvas;
        private Camera _camera;

        private Vector2 _input = Vector2.zero;
        
        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Awake()
        {
            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");
            var center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _camera = null;
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _camera = _canvas.worldCamera;

            var position = RectTransformUtility.WorldToScreenPoint(_camera, background.position);
            var radius = background.sizeDelta / 2;
            Direction = (eventData.position - position) / (radius * _canvas.scaleFactor);
            HandleInput(Direction.magnitude, Direction.normalized, radius, _camera);
            handle.anchoredPosition = Direction * radius * handleRange;
        }

        private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    Direction = normalised;
            }
            else
                Direction = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            Direction = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _camera,
                    out var localPoint)) return Vector2.zero;
            var pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
            return localPoint - (background.anchorMax * _baseRect.sizeDelta) + pivotOffset;

        }

        private void OutputVectorValue()
        {
            SendValueToControl(Direction);
        }
    }
}