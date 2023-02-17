using System;
using UnityEngine;

namespace _Project.Scripts.General.GameEvents
{
    public abstract class GameEvent : MonoBehaviour
    {
        [SerializeField, Range(10.0f, 200.0f)] protected float _duration = 20f;

        protected Action OnStopEvent;

        public abstract void EnableEvent(Action callback);
    }
}
