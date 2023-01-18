using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponEntity", menuName = "Configs/Weapons/WeaponData")]
    public class WeaponEntity : ScriptableObject
    {
        [Range(1.0f, 100.0f)] public float MaxOverheat;
        [Range(1.0f, 50.0f)] public float OverheatPercentPerFire;
        [Range(1.0f, 50.0f)] public float CoolingPerSecond;
        [Range(0.01f, 2.0f)] public float FireRate;
        [Range(1.0f, 10.0f)] public float OverheatDuration;
        [Range(0.5f, 5.0f)] public float ReloadingDuration;
        [Range(5.0f, 100.0f)] public float Damage;
    }
}
