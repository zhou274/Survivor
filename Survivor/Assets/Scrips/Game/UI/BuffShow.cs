using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffShow : MonoBehaviour
{
    public Image WeaponImgae;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Level;
    public Button button;
    public void Init(Sprite image,string title, int level)
    {
        WeaponImgae.sprite = image;
        Title.text = title;
        if(level<=0)
        {
            Level.text = "Î´½âËø";
        }
        else
        {
            Level.text = "µÈ¼¶£º" + level.ToString();
        }
        
    }
}
