using System.Collections;
using _Project.Scripts.Core.Pool;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.General.Utils.Audio
{
    public class CorePoolAudio : CorePoolElement
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private float _duration;

        public void Play()
        {
            gameObject.SetActive(true);
            _source.Play();
            StartCoroutine(OnComplete());
        }
        
        public void Play(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            _source.Play();
            StartCoroutine(OnComplete());
        }
        
        private IEnumerator OnComplete()
        {
            yield return new WaitForSeconds(_duration);
            gameObject.SetActive(false);
            CorePool.Current.Return(this);
        }
    }
}
