using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class FXEmitter : MonoBehaviour, IFXEmitter
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _clickButtonSound;
        [SerializeField] private AudioClip _switchButtonSound;

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
    }

    public interface IFXEmitter : IGameService
    {
        public void PlayButtonSound();
        public void PlaySwitchSound();
    }
}