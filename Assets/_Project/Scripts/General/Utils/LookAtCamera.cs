using UnityEngine;

namespace _Project.Scripts.General.Utils
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;
        private Transform _ownTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
            _ownTransform = transform;
        }

        void Update()
        {
            _ownTransform.LookAt(_cameraTransform);
        }
    }
}