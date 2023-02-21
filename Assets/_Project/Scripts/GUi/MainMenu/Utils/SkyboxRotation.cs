using System.Collections;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Utils
{
    public class SkyboxRotation : MonoBehaviour
    {
        [SerializeField] private Material _skyboxMaterial;
        [SerializeField, Range(100.0f, 240.0f)] private float _duration = 150f;
        
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        private void Awake()
        {
            StartCoroutine(RotateSky());
        }

        private IEnumerator RotateSky()
        {
            while (true)
            {
                yield return SimpleLerp(0f, 360f, _duration);
                yield return SimpleLerp(360f, 0f, _duration);
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        
        private IEnumerator SimpleLerp(float initial, float target, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float current = Mathf.Lerp(initial, target, elapsedTime / duration);
                _skyboxMaterial.SetFloat(Rotation, current);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
