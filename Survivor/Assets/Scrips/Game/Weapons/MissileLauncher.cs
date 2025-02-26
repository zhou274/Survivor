using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject missile;
    public int missleNumber = 3; // 发射方向数量 
    //public float spreadAngle = 90f; // 总扩散角度 
    public float Fireinterval = 1.0f;
    public GameObject target;
    public GameObject BulletManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CreateBullet");
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponentInParent<PhysicsDetector>().target;
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
        while (true) 
        {
            Fireinterval = 5;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            //float angleStep = spreadAngle / directions;
            if (target!=null)
            {
                if(WeaponManager.Instance.MisslileLevel==1)
                {
                    missleNumber = 10;
                    for (int i = 0; i < missleNumber; i++)
                    {
                        target = transform.GetComponentInParent<PhysicsDetector>().target;
                        if(target!=null)
                        {
                            target = transform.GetComponentInParent<PhysicsDetector>().target;
                            Vector2 direction = (target.transform.position - transform.position).normalized;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            Quaternion rotation = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(missile, transform.position, rotation);
                            bullet.GetComponent<Missile>().damage = 10;
                            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * bullet.GetComponent<Missile>().moveSpeed;
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.MissileLaunch, Camera.main.transform.position);
                            bullet.transform.SetParent(BulletManager.transform);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (WeaponManager.Instance.MisslileLevel == 2)
                {
                    //增加数量 提高伤害
                    missleNumber = 15;
                    for (int i = 0; i < missleNumber; i++)
                    {
                        target = transform.GetComponentInParent<PhysicsDetector>().target;
                        if (target!=null)
                        {
                            Vector2 direction = (target.transform.position - transform.position).normalized;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            Quaternion rotation = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(missile, transform.position, rotation);
                            bullet.GetComponent<Missile>().damage = 20;
                            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * bullet.GetComponent<Missile>().moveSpeed;
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.MissileLaunch, Camera.main.transform.position);
                            bullet.transform.SetParent(BulletManager.transform);
                            yield return new WaitForSeconds(0.2f);
                        }
                        
                        
                    }
                }
                else if (WeaponManager.Instance.MisslileLevel == 3)
                {
                    //增加数量 提高伤害
                    missleNumber = 20;
                    for (int i = 0; i < missleNumber; i++)
                    {
                        target = transform.GetComponentInParent<PhysicsDetector>().target;
                        if(target!=null)
                        {
                            Vector2 direction = (target.transform.position - transform.position).normalized;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            Quaternion rotation = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(missile, transform.position, rotation);
                            bullet.GetComponent<Missile>().damage = 40;
                            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * bullet.GetComponent<Missile>().moveSpeed;
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.MissileLaunch, Camera.main.transform.position);
                            bullet.transform.SetParent(BulletManager.transform);
                            yield return new WaitForSeconds(0.2f);
                        }
                        
                    }
                }
                else if (WeaponManager.Instance.MisslileLevel == 4)
                {
                    //增加数量 提高伤害
                    missleNumber = 20;
                    for (int i = 0; i < missleNumber; i++)
                    {
                        target = transform.GetComponentInParent<PhysicsDetector>().target;
                        if(target!=null)
                        {
                            Vector2 direction = (target.transform.position - transform.position).normalized;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            Quaternion rotation = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(missile, transform.position, rotation);
                            bullet.GetComponent<Missile>().damage = 50;
                            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * bullet.GetComponent<Missile>().moveSpeed;
                            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.MissileLaunch, Camera.main.transform.position);
                            bullet.transform.SetParent(BulletManager.transform);
                            yield return new WaitForSeconds(0.2f);
                        }
                        
                    }
                }
            }
            yield return new WaitForSeconds(Fireinterval);
        }
        
    }
}
