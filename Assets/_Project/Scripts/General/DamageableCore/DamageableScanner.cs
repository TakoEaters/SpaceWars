using System;
using System.Collections;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using UnityEngine;

namespace _Project.Scripts.General.DamageableCore
{
    public class DamageableScanner : MonoBehaviour
    {
        [SerializeField, Range(1.0f, 100.0f)] private float _scanRange;
        [SerializeField, Range(0.1f, 2.0f)] private float _delay = 1f;
        public IDamageable LastDetected { get; private set; }
        
        private Coroutine _scanCoroutine;

        public void StartScanning(Action<IDamageable> changeTargetAction, DamageableLayer damageableLayer, Team friendlyTeam)
        {
            if (_scanCoroutine != null) StopCoroutine(_scanCoroutine);
            LastDetected = null;
            _scanCoroutine = StartCoroutine(Scan(changeTargetAction, damageableLayer, friendlyTeam));
        }
        
        public void StartScanning(DamageableLayer damageableLayer, Team friendlyTeam)
        {
            if (_scanCoroutine != null) StopCoroutine(_scanCoroutine);
            LastDetected = null;
            _scanCoroutine = StartCoroutine(Scan(null, damageableLayer, friendlyTeam));
        }

        private IEnumerator Scan(Action<IDamageable> changeTargetAction, DamageableLayer damageableLayer, Team team)
        {
            WaitForSeconds waitingTime = new WaitForSeconds(_delay);
            Transform ownTransform = transform;
            
            while (true)
            {
                IDamageable target = damageableLayer.Scan(ownTransform.position, _scanRange, team);

                if (LastDetected == null || LastDetected.IsInSafeZone || !LastDetected.IsAlive)
                {
                    LastDetected = target == LastDetected ? null : target;
                    changeTargetAction.Invoke(target);
                }
                else if (target != null && target.Priority > LastDetected.Priority)
                {
                    LastDetected = target;
                    changeTargetAction.Invoke(target);
                }

                else if (LastDetected != null && target == null)
                {
                    LastDetected = null;
                    changeTargetAction.Invoke(null);
                }

                yield return waitingTime;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void StopScanning()
        {
            StopCoroutine(_scanCoroutine);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _scanRange);
        }
    }
}