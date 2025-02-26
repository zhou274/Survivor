using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
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
            Fireinterval = 5f;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            if (target != null)
            {
                if(WeaponManager.Instance.RocketLevel==1)
                {
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    GameObject rocket = Instantiate(bullet, transform.position, rotation);
                    rocket.GetComponent<Rocket>().BaseDamage =20;
                    rocket.GetComponent<Rocket>().explosionRadius = 0.7f;
                    rocket.GetComponent<Rocket>().explpdeScale = 1;
                    rocket.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * rocket.GetComponent<Rocket>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RocketLaunch, Camera.main.transform.position);
                    rocket.transform.SetParent(bulletManager.transform);
                }
                else if(WeaponManager.Instance.RocketLevel==2)
                {
                    //伤害增加
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    GameObject rocket = Instantiate(bullet, transform.position, rotation);
                    rocket.transform.localScale = rocket.transform.localScale * 2;
                    rocket.GetComponent<Rocket>().BaseDamage = 40;
                    rocket.GetComponent<Rocket>().explosionRadius = 0.7f;
                    rocket.GetComponent<Rocket>().explpdeScale = 1;
                    rocket.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * rocket.GetComponent<Rocket>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RocketLaunch, Camera.main.transform.position);
                    rocket.transform.SetParent(bulletManager.transform);
                }
                else if (WeaponManager.Instance.RocketLevel == 3)
                {
                    //伤害增加 范围增大
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    GameObject rocket = Instantiate(bullet, transform.position, rotation);
                    rocket.transform.localScale = rocket.transform.localScale * 2;
                    rocket.GetComponent<Rocket>().BaseDamage = 80;
                    rocket.GetComponent<Rocket>().explosionRadius = 1.05f;
                    rocket.GetComponent<Rocket>().explpdeScale = 1.5f;
                    rocket.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * rocket.GetComponent<Rocket>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RocketLaunch, Camera.main.transform.position);
                    rocket.transform.SetParent(bulletManager.transform);
                }
                else if (WeaponManager.Instance.RocketLevel == 4)
                {
                    //伤害增加,范围增大
                    Vector2 direction = (target.transform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    GameObject rocket = Instantiate(bullet, transform.position, rotation);
                    rocket.transform.localScale = rocket.transform.localScale * 2;
                    rocket.GetComponent<Rocket>().BaseDamage = 120;
                    rocket.GetComponent<Rocket>().explosionRadius = 1.4f;
                    rocket.GetComponent<Rocket>().explpdeScale = 2f;
                    rocket.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * rocket.GetComponent<Rocket>().moveSpeed;
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RocketLaunch, Camera.main.transform.position);
                    rocket.transform.SetParent(bulletManager.transform);
                }

            }
            yield return new WaitForSeconds(Fireinterval);

        }


    }
}
