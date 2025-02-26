using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBallLauncher : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;
    private float Fireinterval = 1f;
    Vector3 direction;
    public Transform bulletManager;
    //Start is called before the first frame update
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
            Fireinterval = 4f;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            if (target != null)
            {
                if (WeaponManager.Instance.FootBallLevel == 1)
                {
                    GameObject ball = Instantiate(bullet, transform.position, Quaternion.identity);
                    ball.GetComponent<FootBall>().maxBounces = 3;
                    ball.GetComponent<FootBall>().damage = 10;
                    direction = target.transform.position - transform.position;
                    ball.GetComponent<Rigidbody2D>().AddForce(direction * ball.GetComponent<FootBall>().moveSpeed, ForceMode2D.Impulse);
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.FootBallLaunch, Camera.main.transform.position);
                    ball.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.FootBallLevel == 2)
                {
                    //减少冷却，增加弹跳次数 增加伤害
                    GameObject ball = Instantiate(bullet, transform.position, Quaternion.identity);
                    ball.GetComponent<FootBall>().maxBounces = 5;
                    ball.GetComponent<FootBall>().damage = 20;
                    direction = target.transform.position - transform.position;
                    ball.GetComponent<Rigidbody2D>().AddForce(direction * ball.GetComponent<FootBall>().moveSpeed, ForceMode2D.Impulse);
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.FootBallLaunch, Camera.main.transform.position);
                    ball.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.FootBallLevel == 3)
                {
                    //增加弹跳次数 增加伤害
                    GameObject ball = Instantiate(bullet, transform.position, Quaternion.identity);
                    ball.GetComponent<FootBall>().maxBounces = 7;
                    ball.GetComponent<FootBall>().damage = 40;
                    direction = target.transform.position - transform.position;
                    ball.GetComponent<Rigidbody2D>().AddForce(direction * ball.GetComponent<FootBall>().moveSpeed, ForceMode2D.Impulse);
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.FootBallLaunch, Camera.main.transform.position);
                    ball.transform.SetParent(bulletManager);
                }
                else if (WeaponManager.Instance.FootBallLevel == 4)
                {
                    //增加弹跳次数 增加伤害
                    GameObject ball = Instantiate(bullet, transform.position, Quaternion.identity);
                    ball.GetComponent<FootBall>().maxBounces = 9;
                    ball.GetComponent<FootBall>().damage = 80;
                    direction = target.transform.position - transform.position;
                    ball.GetComponent<Rigidbody2D>().AddForce(direction * ball.GetComponent<FootBall>().moveSpeed, ForceMode2D.Impulse);
                    SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.FootBallLaunch, Camera.main.transform.position);
                    ball.transform.SetParent(bulletManager);
                }
            }
            yield return new WaitForSeconds(Fireinterval);

        }


    }
}
