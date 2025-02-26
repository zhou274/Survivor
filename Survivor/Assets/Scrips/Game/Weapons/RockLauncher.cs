using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLauncher : MonoBehaviour
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
    }
    IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            Fireinterval = 3f;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            if (WeaponManager.Instance.RockLevel==1)
            {
                GameObject rock = Instantiate(bullet, transform.position, Quaternion.identity);
                rock.GetComponent<Rock>().damage = 10;
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.up * rock.GetComponent<Rock>().moveSpeed;
                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RockLaunch, Camera.main.transform.position);
                rock.transform.SetParent(bulletManager.transform);
            }
            else if (WeaponManager.Instance.RockLevel == 2)
            {
                //ÉËº¦±ä¸ß£¬ÀäÈ´¼ò¶Ì
                GameObject rock = Instantiate(bullet, transform.position, Quaternion.identity);
                rock.GetComponent<Rock>().damage = 20;
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.up * rock.GetComponent<Rock>().moveSpeed;
                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RockLaunch, Camera.main.transform.position);
                rock.transform.SetParent(bulletManager.transform);
            }
            else if(WeaponManager.Instance.RockLevel==3)
            {
                //Ìå»ý±ä´ó2±¶£¬ÉËº¦·­±¶£¬ÀäÈ´Ð¡·ù¼ò¶Ì
                GameObject rock = Instantiate(bullet, transform.position, Quaternion.identity);
                rock.transform.localScale = rock.transform.localScale * 2;
                rock.GetComponent<Rock>().damage = 40;
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.up * rock.GetComponent<Rock>().moveSpeed;
                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RockLaunch, Camera.main.transform.position);
                rock.transform.SetParent(bulletManager.transform);
            }
            else if(WeaponManager.Instance.RockLevel == 4)
            {
                //Ìå»ý±ä´ó3Èý±¶£¬ÉËº¦·­±¶
                GameObject rock = Instantiate(bullet, transform.position, Quaternion.identity);
                rock.transform.localScale = rock.transform.localScale * 3;
                rock.GetComponent<Rock>().damage = 80;
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.up * rock.GetComponent<Rock>().moveSpeed;
                SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RockLaunch, Camera.main.transform.position);
                rock.transform.SetParent(bulletManager.transform);
            }
            yield return new WaitForSeconds(Fireinterval);

        }


    }
}
