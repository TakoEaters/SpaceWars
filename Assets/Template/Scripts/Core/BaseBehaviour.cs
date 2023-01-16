using System;
using System.Reflection;
using UnityEngine;

namespace Template.Scripts.Core
{
    public class BaseBehaviour : MonoBehaviour
    {
        #region Local

        private void Awake()
        {
            Transform = transform;
            Fix();
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        #endregion

        #region API

        protected Transform Transform { get; private set; }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnStart()
        {
        }

        #endregion

        #region DI_UTILS

        private void Fix()
        {
            var fields = GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (var info in fields)
            {
                if (info.GetCustomAttributes(typeof(Get), false).Length > 0)
                {
                    var type = info.FieldType;
                    var component = GetComponent(type);
                    info.SetValue(this, component);

                }
                else if (info.GetCustomAttributes(typeof(Find), false).Length > 0)
                {
                    var type = info.FieldType;
                    var component = FindObjectOfType(type);
                    info.SetValue(this, component);
                }
                else if (info.GetCustomAttributes(typeof(GetChild), false).Length > 0)
                {
                    var type = info.FieldType;
                    var component = GetComponentInChildren(type);
                    info.SetValue(this, component);
                }
            }

        }

        #endregion
    }

    public class Get : Attribute
    {
    }

    public class GetChild : Attribute
    {
    }

    public class Find : Attribute
    {
    }
}
