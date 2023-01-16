using System;
using UnityEngine;

namespace _Project.Scripts.Core.SaveSystem
{
    public static class PlayerMoney
    {
        public const string PlayerMoneyID = "PlayerMoney";
        public static Action OnChange;

        public static void Reset()
        {
            OnChange = null;
        }

        public static int GetValue()
        {
            return SaveManager.GetValue(PlayerMoneyID, 20);
        }

        public static void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, int.MaxValue);
            SaveManager.SetValue(PlayerMoneyID, value);
            OnChange?.Invoke();
        }

        public static bool IsPurchasable(int price)
        {
            return GetValue() >= price;
        }

        public static void Increment(int value = 1)
        {
            var finalValue = GetValue() + value;
            SetValue(finalValue);
            OnChange?.Invoke();
        }
    }
}
