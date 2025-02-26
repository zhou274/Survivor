using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coin");
    }
    public static void AddCoins(int amount)
    {
        int coins=GetCoins()+amount;
        PlayerPrefs.SetInt("Coin", coins);
    }    
    public static void ReduceCoins(int amount)
    {
        int coins = GetCoins() - amount;
        if(coins<=0)
        {
            coins = 0;
        }
        PlayerPrefs.SetInt("Coin", coins);
    }
}
