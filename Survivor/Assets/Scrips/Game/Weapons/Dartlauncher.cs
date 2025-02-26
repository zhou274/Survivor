using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartlauncher: MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;
    private float Fireinterval = 1f;
    Vector3 direction;
    public Transform bulletManager;
    // Start is called before the first frame update

    //void Start()
    //{
    //    StartCoroutine(CreateBullet());
    //}
    private void OnEnable()
    {
        StartCoroutine("CreateBullet");
    }
    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponentInParent<PhysicsDetector>().target;
        
        //Debug.Log("检测到目标");
    }
    public void startCreate()
    {
        StartCoroutine("CreateBullet");
    }
    public void stopCreate()
    {
        StopCoroutine("CreateBullet");
    }
    IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            Fireinterval = 1f;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            if (target!=null)
            {
                if(WeaponManager.Instance.DartLevel==1)
                {
                    GameObject biao = Instantiate(bullet, transform.position, Quaternion.identity);
                    biao.GetComponent<Dart>().damage = 10;
                    direction = target.transform.position - transform.position;
                    biao.GetComponent<Rigidbody2D>().AddForce(direction * biao.GetComponent<Dart>().moveSpeed, ForceMode2D.Impulse);
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.DartLaunch, Camera.main.transform.position);
                    biao.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.DartLevel == 2)
                {
                    //攻速提高 发射飞镖数量增加 攻击力增加
                    for(int i=0;i<2;i++)
                    {
                        if(target!=null)
                        {
                            GameObject biao = Instantiate(bullet, transform.position, Quaternion.identity);
                            biao.GetComponent<Dart>().damage = 20;
                            target = transform.GetComponentInParent<PhysicsDetector>().target;
                            direction = target.transform.position - transform.position;
                            biao.GetComponent<Rigidbody2D>().AddForce(direction * biao.GetComponent<Dart>().moveSpeed, ForceMode2D.Impulse);
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.DartLaunch, Camera.main.transform.position);
                            biao.transform.SetParent(bulletManager);
                            yield return new WaitForSeconds(0.1f);
                        }
                        
                    }
                }
                else if (WeaponManager.Instance.DartLevel == 3)
                {
                    //攻速提高 发射飞镖数量增加 攻击力增加
                    for (int i = 0; i < 3; i++)
                    {
                        if(target!=null)
                        {
                            GameObject biao = Instantiate(bullet, transform.position, Quaternion.identity);
                            biao.GetComponent<Dart>().damage = 35;
                            target = transform.GetComponentInParent<PhysicsDetector>().target;
                            direction = target.transform.position - transform.position;
                            biao.GetComponent<Rigidbody2D>().AddForce(direction * biao.GetComponent<Dart>().moveSpeed, ForceMode2D.Impulse);
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.DartLaunch, Camera.main.transform.position);
                            biao.transform.SetParent(bulletManager);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                }
                else if (WeaponManager.Instance.DartLevel == 4)
                {
                    //攻速提高 发射飞镖数量增加 攻击力增加
                    for (int i = 0; i < 4; i++)
                    {
                        if(target!=null)
                        {
                            GameObject biao = Instantiate(bullet, transform.position, Quaternion.identity);
                            biao.GetComponent<Dart>().damage = 50;
                            target = transform.GetComponentInParent<PhysicsDetector>().target;
                            if(target!=null)
                            {
                                direction = target.transform.position - transform.position;
                                biao.GetComponent<Rigidbody2D>().AddForce(direction * biao.GetComponent<Dart>().moveSpeed, ForceMode2D.Impulse);
                                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.DartLaunch, Camera.main.transform.position);
                                biao.transform.SetParent(bulletManager);
                              
                            }
                            else
                            {
                                biao.GetComponent<Rigidbody2D>().AddForce(Vector2.right * biao.GetComponent<Dart>().moveSpeed, ForceMode2D.Impulse);
                                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.DartLaunch, Camera.main.transform.position);
                                biao.transform.SetParent(bulletManager);
                            }
                            yield return new WaitForSeconds(0.1f);
                        }
                        
                    }
                }
            }
            yield return new WaitForSeconds(Fireinterval);

        }
        

    }
}
