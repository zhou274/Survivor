using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangLauncher : MonoBehaviour
{
    public GameObject boomerang;
    public GameObject target;
    //private float moveSpeed = 5;
    Vector3 direction;
    private float Fireinterval=1f;
    public Transform bulletManager;
    private void OnEnable()
    {
        StartCoroutine ("CreateBullet");
    }
    public void startCreate()
    {
        StartCoroutine("CreateBullet");
    }
    public void stopCreate()
    {
        StopCoroutine("CreateBullet");
    }
    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponentInParent<PhysicsDetector>().target;
        //Debug.Log("检测到目标");
    }
    IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            Fireinterval = 4f;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            if (target != null)
            {
                if(WeaponManager.Instance.BoomerangLevel==1)
                {
                    GameObject Boomeran = Instantiate(boomerang, transform.position, Quaternion.identity);
                    boomerang.GetComponent<Boomerang>().damage = 10;
                    direction = target.transform.position - transform.position;
                    Boomeran.GetComponent<Rigidbody2D>().velocity = direction.normalized * boomerang.GetComponent<Boomerang>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangLaunch, Camera.main.transform.position);
                    Boomeran.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.BoomerangLevel == 2)
                {
                    //伤害增加 模型变大
                    GameObject Boomeran = Instantiate(boomerang, transform.position, Quaternion.identity);
                    Boomeran.transform.localScale = Boomeran.transform.localScale * 1.5f;
                    Boomeran.GetComponent<Boomerang>().damage = 20;
                    direction = target.transform.position - transform.position;
                    Boomeran.GetComponent<Rigidbody2D>().velocity = direction.normalized * Boomeran.GetComponent<Boomerang>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangLaunch, Camera.main.transform.position);
                    Boomeran.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.BoomerangLevel == 3)
                {
                    //伤害增加 模型变大
                    GameObject Boomeran = Instantiate(boomerang, transform.position, Quaternion.identity);
                    Boomeran.transform.localScale = Boomeran.transform.localScale * 2f;
                    Boomeran.GetComponent<Boomerang>().damage = 40;
                    direction = target.transform.position - transform.position;
                    Boomeran.GetComponent<Rigidbody2D>().velocity = direction.normalized * Boomeran.GetComponent<Boomerang>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangLaunch, Camera.main.transform.position);
                    Boomeran.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.BoomerangLevel == 4)
                {
                    //数量增加
                    GameObject Boomeran = Instantiate(boomerang, transform.position, Quaternion.identity);
                    Boomeran.transform.localScale = Boomeran.transform.localScale * 2f;
                    boomerang.GetComponent<Boomerang>().damage = 80;
                    direction = target.transform.position - transform.position;
                    Boomeran.GetComponent<Rigidbody2D>().velocity = direction.normalized * Boomeran.GetComponent<Boomerang>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangLaunch, Camera.main.transform.position);
                    Boomeran.transform.SetParent(bulletManager);
                    
                    GameObject Boomeran2 = Instantiate(boomerang, transform.position, Quaternion.identity);
                    Boomeran2.transform.localScale = Boomeran2.transform.localScale * 2f;
                    Boomeran2.GetComponent<Boomerang>().damage = 80;
                    Boomeran2.GetComponent<Rigidbody2D>().velocity = -direction.normalized * Boomeran2.GetComponent<Boomerang>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangLaunch, Camera.main.transform.position,1f);
                    Boomeran2.transform.SetParent(bulletManager);
                }
            }
            yield return new WaitForSeconds(Fireinterval);

        }


    }
}
