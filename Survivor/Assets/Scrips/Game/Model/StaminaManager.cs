using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public static int GetStamina()
    {
        return PlayerPrefs.GetInt("Stamina");
    }
    public static void AddStamina(int amount)
    {
        int stamina = PlayerPrefs.GetInt("Stamina")+amount;
        if(stamina>=60)
        {
            stamina = 60;
        }
        PlayerPrefs.SetInt("Stamina", stamina);
    }
    public static void ReduceStamina(int amount)
    {
        int stamina = PlayerPrefs.GetInt("Stamina") - amount;
        if(stamina<=0)
        {
            stamina = 0;
        }
        PlayerPrefs.SetInt("Stamina", stamina);
    }
}
