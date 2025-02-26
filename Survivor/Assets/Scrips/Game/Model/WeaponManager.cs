using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public int CooldownLevel;
    public float CooldownTimePersent;
    public int HoldLevel;
    public float HoldTimePersent;
    public int RockLevel;
    public int RocketLevel;
    public int MisslileLevel;
    public int FootBallLevel;
    public int DartLevel;
    public int TopLevel;
    public int RangeLevel;
    public int BoomerangLevel;
    public Action TopLevelChanged;
    private void Awake()
    {
        Instance = this;
    }
    public void StopWeapon()
    {
        FindObjectOfType<BoomerangLauncher>().stopCreate();
        FindObjectOfType<Dartlauncher>().stopCreate();
        FindObjectOfType<FootBallLauncher>().stopCreate();
        FindObjectOfType<MissileLauncher>().stopCreate();
        FindObjectOfType<RocketLauncher>().stopCreate();
        FindObjectOfType<RockLauncher>().stopCreate();
    }
    public void StartWeapon()
    {
        FindObjectOfType<BoomerangLauncher>().startCreate();
        FindObjectOfType<Dartlauncher>().startCreate();
        FindObjectOfType<FootBallLauncher>().startCreate();
        FindObjectOfType<MissileLauncher>().startCreate();
        FindObjectOfType<RocketLauncher>().startCreate();
        FindObjectOfType<RockLauncher>().startCreate();
    }
}
