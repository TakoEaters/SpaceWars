using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ShootingSystem : MonoBehaviour
{

    MovementInput input;

    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;
    [SerializeField] CinemachineFreeLook freeLookCamera;
    CinemachineImpulseSource impulseSource;

    private PlayerInputs _inputs;
    
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

        if (_inputs.IsShooting)
        {
            VisualPolish();
            input.RotateToCamera(transform);
        }

        if (_inputs.IsShootingPressed)
        {
            inkParticle.Play();
        }
        else if (_inputs.IsShootingReleased)
        {
            inkParticle.Stop();
        }

        parentController.localEulerAngles
            = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? RemapCamera(freeLookCamera.m_YAxis.Value, 0, 1, -25, 25) : 0, .3f), angle.y, angle.z);
    }

    void VisualPolish()
    {
        if (!DOTween.IsTweening(parentController))
        {
            parentController.DOComplete();
            Vector3 forward = -parentController.forward;
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
}
