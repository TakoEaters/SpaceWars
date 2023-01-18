using _Project.Scripts.Core.Pool;
using _Project.Scripts.General.Utils.Audio;
using _Project.Scripts.Player.WeaponsSystem;
using Cinemachine;
using CustomAssets.Jammo_Character.Scripts;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ColorIt
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private WeaponEntity _data;
        [SerializeField] private float _sfxThreshold = 1f;
        [SerializeField] private CorePoolAudio _clip;

        MovementInput input;

        [SerializeField] ParticleSystem inkParticle;
        [SerializeField] Transform parentController;
        [SerializeField] Transform splatGunNozzle;
        [SerializeField] CinemachineFreeLook freeLookCamera;
        CinemachineImpulseSource impulseSource;

        private PlayerInputs _inputs;
        private float _lastShootingTime;
    
        void Start()
        {
            input = GetComponent<MovementInput>();
            _inputs = GetComponent<PlayerInputs>(); 
            impulseSource = freeLookCamera.GetComponent<CinemachineImpulseSource>();
        }

        private void Update()
        {
            Vector3 angle = parentController.localEulerAngles;
            input.blockRotationPlayer = _inputs.IsShooting;
            bool pressing = _inputs.IsShooting;



            Fire();
            OverheatUpdate();
            if (_inputs.IsShootingPressed && !IsOverheat)
            {
                inkParticle.Play();
            }
            else if (_inputs.IsShootingReleased)
            {
                inkParticle.Stop();
            }

            parentController.localEulerAngles
                = new Vector3(
                    Mathf.LerpAngle(parentController.localEulerAngles.x,
                        pressing ? RemapCamera(freeLookCamera.m_YAxis.Value, 0, 1, -25, 25) : 0, .3f), angle.y,
                    angle.z);
        }

        private void VisualPolish()
        {
            if (!DOTween.IsTweening(parentController))
            {
                parentController.DOComplete();
                Vector3 localPos = parentController.localPosition;
                parentController.DOLocalMove(localPos - new Vector3(0, 0, .2f), .03f)
                    .OnComplete(() => parentController.DOLocalMove(localPos, .1f).SetEase(Ease.OutSine));

                impulseSource.GenerateImpulse();
            }

            if (!DOTween.IsTweening(splatGunNozzle))
            {
                splatGunNozzle.DOComplete();
                splatGunNozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
            }
        }

        float RemapCamera(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        
        
        #region Overheat

        public float LastCoolingTime;
        public float Overheat;
        public bool IsOverheat;
        
        private void Fire()
        {
            if (_inputs.IsShooting)
            {
                VisualPolish();
                input.RotateToCamera(transform);
                if (Time.time - _lastShootingTime >=  _data.FireRate && !IsOverheat)
                {
                    Overheat = Mathf.Clamp(Overheat + _data.OverheatDuration, 0f, 100f);
                    var clip = CorePool.Current.Get(_clip);
                    clip.Play();
                    _lastShootingTime = Time.time;
                }
            }
        }
        
        private void OverheatUpdate()
        {
            if (Time.time - LastCoolingTime > 1f && Time.time - _lastShootingTime > _data.FireRate + 1f)
            {
                Overheat = Mathf.Clamp(Overheat - _data.CoolingPerSecond, 0f, 100f);
                LastCoolingTime = Time.time;
            }

            if (Overheat >= 99) IsOverheat = true;
            else if (Overheat < _data.MaxOverheat) IsOverheat = false;
            if (IsOverheat && _sentOverheatEvent == false)
            {
                _sentOverheatEvent = true;
                OnOverheat();
           //     _onOverheat?.Invoke();
            }

            if (Overheat < 5f)
            {
                _sentOverheatEvent = false;
            }
            // if (_isPlayer)
            // {
            //     _overheatIndicator.UpdateValue(Overheat / 100f);
            //     _overheatIndicator.SetOverheat(IsOverheat);
            // }
        }

        private void OnOverheat()
        {
            inkParticle.Stop();
        }

        private bool _sentOverheatEvent;

        public void Cool()
        {
            Overheat = 0f;
            _sentOverheatEvent = false;
         //   _onCool?.Invoke();
        }

        #endregion
    }
}
