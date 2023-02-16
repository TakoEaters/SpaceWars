using System;
using _Project.Scripts.General.Extensions;
using UnityEngine;

namespace _Project.Scripts
{
    public class TestFollower : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 3f;

        [SerializeField] private float _yClamps;

        private Vector3 _initialMousePosition;
        private Vector3 _currentMousePosition;
        private Vector3 _direction;

        private void Awake()
        {
            _initialMousePosition = Input.mousePosition;
        }


        private void Update()
        {
            Vector3 delta;
            Vector3 lastPosition = Vector3.zero;


            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                delta = Input.mousePosition - lastPosition;
                lastPosition = Input.mousePosition;
                transform.position += Vector3.zero.AddY(delta.y) * _lerpSpeed * Time.deltaTime;
            }
        }
    }



}
    
