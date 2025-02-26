using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; 
    public AudioClipRefsSO audioClipRefsSO;
    public void Awake()
    {
        instance = this;
    }
    public void PlaySound(AudioClip clip,Vector3 position,float volume=1.0f)
    {
        AudioSource.PlayClipAtPoint(clip,position,volume);
    }
    public void Start()
    {
        PlaySound(audioClipRefsSO.DartLaunch,transform.position);
    }
    public void BtnClick()
    {
        PlaySound(audioClipRefsSO.BtnClick, Camera.main.transform.position, 1.0f);
    }
}
