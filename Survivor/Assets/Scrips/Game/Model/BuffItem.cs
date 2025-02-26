using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObject/Buff����", order = 0)]
public class BuffItem : ScriptableObject
{
    public string Title;
    public Sprite BuffImage;
    public int Level;
}
