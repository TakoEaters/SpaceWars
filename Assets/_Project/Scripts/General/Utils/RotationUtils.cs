using System.Collections;
using UnityEngine;

namespace _Project.Scripts.General.Extensions
{
    public static class RotationUtils
    {
        public static void LookAt(this Transform ownTransform, Vector3 targetPosition, float speed)
        {
            Vector3 direction = (targetPosition - ownTransform.position).normalized;
            if (direction == Vector3.zero) return;
            ownTransform.rotation = Quaternion.Slerp(ownTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
        }

        public static void LookAt(this Transform ownTransform, Transform targetTransform, float speed)
        {
            Vector3 direction = (targetTransform.position - ownTransform.position).normalized;
            ownTransform.rotation = Quaternion.Slerp(ownTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
        }

        public static IEnumerator SlerpRotation(this Transform ownTransform, Vector3 targetPos, float speed)
        {
            float time = 0f;
            while (time < 1f)
            {
                Quaternion nextRotation = Quaternion.LookRotation(targetPos - ownTransform.position);
                ownTransform.rotation = Quaternion.Slerp(ownTransform.rotation, nextRotation, time);
                
                time += Time.deltaTime * speed;
                yield return null;
            }
        }
        
        public static IEnumerator RotateForward(this Transform ownTransform, Vector3 forwardDirection, float speed, bool isReceivePause = true)
        {
            float time = 0f;
            while (time < 1f)
            {
                ownTransform.forward = Vector3.MoveTowards(ownTransform.forward, forwardDirection
                    ,time);
                
                time += Time.deltaTime * speed;
                yield return null;
            }
        }
        
        public static float GetRotationDirection(this float current, float target)
        {
            var deltaAngle = Mathf.DeltaAngle(current, target);
            return deltaAngle / 180f;
        }

        public static float GetYRotation(this Vector3 direction)
        {
            if (direction == Vector3.zero) return 0f;
            var rotation = Quaternion.LookRotation(direction);
            return rotation.eulerAngles.y;
        }

        public static Quaternion GetRotation(this Vector3 direction)
        {
            return direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);
        }

        public static Quaternion RemoveXZ(this Quaternion rotation)
        {
            return Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y, 0));
        }

        public static Vector3 GetRandomPositionOnCircle(this Vector3 center, float radius)
        {
            Vector2 positionOnCircle = Random.insideUnitCircle.normalized * radius;
            center.x += positionOnCircle.x;
            center.z += positionOnCircle.y;
            return center;
        }
        public static Vector3 GetRandomPositionInCircle(this Vector3 center, float radius)
        {
            var positionOnCircle = Random.insideUnitCircle * radius;
            center.x += positionOnCircle.x;
            center.z += positionOnCircle.y;
            return center;
        }
    }
}