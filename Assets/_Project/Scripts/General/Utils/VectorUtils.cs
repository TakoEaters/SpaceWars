using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.General.Extensions
{
    public static class VectorUtils
    {
        public static int CalculateMultiplier(float initial, float multiplier, int level)
        {
            var m = Mathf.Pow(multiplier, level);
            return (int)(initial * m);
        }
        
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            var closest = Vector3.zero;
            var shortestDistance = Mathf.Infinity;

            foreach (var otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest; 
        }
        
        public static Vector3 GetMidPoint(this Vector3 first, Vector3 second)
        {
            return new Vector3((first.x + second.x) * 0.5f, (first.y + second.y) * 0.5f, (first.z + second.z) * 0.5f);
        }

        public static float GetDistanceTo(this Vector3 first, Vector3 second)
        {
            return Vector3.Distance(first, second);
        }
       
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
     
        public static Vector3 ToVector3(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
      
        public static Vector3 SetX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }
       
        public static Vector3 SetY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }
       
        public static Vector3 SetZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
       
        public static Vector2 SetX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }
     
        public static Vector2 SetY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }
       
        public static Vector3 SetZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
   
        public static Vector3 AddX(this Vector3 v, float x)
        {
            return new Vector3(v.x + x, v.y, v.z);
        }
     
        public static Vector3 AddY(this Vector3 v, float y)
        {
            return new Vector3(v.x, v.y + y, v.z);
        }

        public static Vector3 AddZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, v.z + z);
        }
 
        public static Vector2 AddX(this Vector2 v, float x)
        {
            return new Vector2(v.x + x, v.y);
        }
      
        public static Vector2 AddY(this Vector2 v, float y)
        {
            return new Vector2(v.x, v.y + y);
        }

        public static IEnumerator Scale(this Transform ownTransform, Vector3 scaleValue, float duration)
        {
            float elapsedTime = 0f;

            Vector3 initialScale = ownTransform.localScale;

            while (elapsedTime < duration)
            {
                ownTransform.localScale = Vector3.Lerp(initialScale, scaleValue, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}