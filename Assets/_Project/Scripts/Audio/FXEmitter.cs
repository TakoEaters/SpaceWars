using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class FXEmitter : MonoBehaviour, IFXEmitter
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _clickButtonSound;
        [SerializeField] private AudioClip _switchButtonSound;
        [SerializeField] private AudioClip _successfulPurchaseSound;
        [SerializeField] private AudioClip _failedPurchaseSound;
        [SerializeField] private AudioClip _countSound;

        public void Register()
        {
            ServiceLocator.Current.Register<IFXEmitter>(this);
        }

        public void PlayButtonSound()
        {
            _source.clip = _clickButtonSound;
            _source.Play();
        }

        public void PlaySwitchSound()
        {
            _source.clip = _switchButtonSound;
            _source.Play();
        }

        public void PlaySuccessfulPurchaseSound()
        {
            _source.clip = _successfulPurchaseSound;
            _source.Play();
        }

        public void PlayFailedPurchaseSound()
        {
            _source.clip = _failedPurchaseSound;
            _source.Play();
        }

        public void PlayCountSound()
        {
            _source.clip = _countSound;
            _source.Play();
        }
    }

    public interface IFXEmitter : IGameService
    {
        public void PlayButtonSound();
        public void PlaySwitchSound();
        public void PlaySuccessfulPurchaseSound();
        public void PlayFailedPurchaseSound();
        public void PlayCountSound();
    }
}