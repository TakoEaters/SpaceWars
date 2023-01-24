using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponEntity", menuName = "Configs/Weapons/WeaponData")]
    public class WeaponEntity : ScriptableObject
    {
        [Range(0, 5)] public int ID;
        [Range(1.0f, 100.0f)] public float MaxOverheat;
        [Range(1.0f, 50.0f)] public float CoolingPerSecond;
        [Range(0.01f, 2.0f)] public float FireRate;
        [Range(0.1f, 3.0f)] public float OverheatAdditive = 1.25f;
        [Range(5.0f, 100.0f)] public float Damage;
    }
}
