using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.General;
using _Project.Scripts.General.Utils.Audio;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class DrawController : MonoBehaviour
    {
        public Color paintColor;

        public float minRadius = 0.05f;
        public float maxRadius = 0.2f;
        public float strength = 1;
        public float hardness = 1;


        [SerializeField] private int _distance = 2;
        private int _currentTime = 0;
        private int _damage;

        private void Start()
        {
            paintColor = ServiceLocator.Current.Get<IPaintColor>().CurrentColor;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void OnPaint(GameObject other, Vector3 pos)
        {
            if (other.TryGetComponent(out Paintable p))
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 offset = Random.insideUnitCircle;
                    var pnwos = pos.OffsetX(offset.x * _distance).OffsetZ(offset.y * _distance);
                    DrawPoint(p, pnwos);
                }
            }
        }

        private void DrawPoint(Paintable paint, Vector3 position)
        {
            float radius = Random.Range(minRadius, maxRadius);
            PaintManager.instance.paint(paint, position, radius, hardness, strength, paintColor);   
        }
    }
}
