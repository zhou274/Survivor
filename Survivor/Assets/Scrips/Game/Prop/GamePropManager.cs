using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePropManager : MonoBehaviour
{
    public static GamePropManager instance; 
    public static Action UseBomb;
    public static Action UseDrumstick;
    public static Action UseMagnet;
    private void Awake()
    {
        instance = this;
    }
    public  int GetBombNumber()
    {
        return PlayerPrefs.GetInt("BombNumber");
    }
    public int GetDrumstickNumber()
    {
        return PlayerPrefs.GetInt("Drumstick");
    }
    public int GetMagnetNumber()
    {
        return PlayerPrefs.GetInt("Magnet");
    }
    public void AddBomb(int amount)
    {
        int number=GetBombNumber()+amount;
        PlayerPrefs.SetInt("BombNumber", number);
    }
    public void ReduceBomb(int amount)
    {
        int number = GetBombNumber() - amount;
        PlayerPrefs.SetInt("BombNumber", number);
    }
    public void AddDrumstick(int amount)
    {
        int number = GetDrumstickNumber() + amount;
        PlayerPrefs.SetInt("Drumstick", number);
    }
    public void ReduceDrumstick(int amount)
    {
        int number = GetDrumstickNumber() - amount;
        PlayerPrefs.SetInt("Drumstick", number);
    }
    public void AddMagnet(int amount)
    {
        int number = GetMagnetNumber() + amount;
        PlayerPrefs.SetInt("Magnet", number);
    }
    public void ReduceMagnet(int amount)
    {
        int number = GetMagnetNumber() - amount;
        PlayerPrefs.SetInt("Magnet", number);
    }
}
