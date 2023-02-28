using _Project.Scripts.Core.LocatorServices;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Common.Sensitivity
{
    public class SensitivityApplier : MonoBehaviour, ISensitivity
    {
        private CinemachinePOV _pov;
        private float _horizontalValue;
        private float _verticalValue;

        private void Start()
        {
            _pov = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
            _horizontalValue = _pov.m_HorizontalAxis.m_MaxSpeed;
            _verticalValue = _pov.m_VerticalAxis.m_MaxSpeed;
            ServiceLocator.Current.Get<ISensitivityManager>().Subscribe(this);
        }

        public void SetSensitivity(float percentage)
        { 
            if (!_pov) _pov = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
            float horizontalPercentage = Mathf.Lerp(0f, _horizontalValue, percentage);
            float verticalPercentage = Mathf.Lerp(0f, _verticalValue, percentage);
            _pov.m_HorizontalAxis.m_MaxSpeed = horizontalPercentage;
            _pov.m_VerticalAxis.m_MaxSpeed = verticalPercentage;
        }

        private void OnDestroy()
        {
            ServiceLocator.Current.Get<ISensitivityManager>().UnSubscribe(this);
        }
    }


    public interface ISensitivity
    {
        public void SetSensitivity(float percentage);
    }
}
