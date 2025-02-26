using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : Enemy
{
    public bool isAttack;
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public int numberOfBullets = 8; // �ӵ�����
    public float cooldown = 1f; // ������ȴʱ��
    public bool IsReturnToMove=false;
    
    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootAtRandomIntervals());
    }
    public override void Move()
    {
        //�����ƶ�
        if (isDead == false && player!=null &&isAttack==false)
        {
            animator.SetBool("IsWalk", true);
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            if (direction.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (IsReturnToMove == false)
            {
                StartCoroutine(ReturnToMove());
            }
            rb.velocity = Vector3.zero;
            animator.SetBool("IsWalk", false);
            
        }
    }
    public override void Dead()
    {
        FindObjectOfType<EnemyManager>().isBossAlive = false;
        FindObjectOfType<EnemyManager>().StartCreate();
        base.Dead();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnDemage(int i, int force)
    {
        hp -= i;
        if (hp > 0)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            //animator.SetTrigger("IsHurt");
        }
        if (hp <= 0)
        {
            isDead = true;
            Dead();
        }
    }
    public void ShootAllDirection()
    {
        isAttack = true;
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;
        SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RocketLaunch, Camera.main.transform.position);
        for (int i = 0; i < numberOfBullets; i++)
        {
            // �����ӵ��ķ���
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            // ʵ�����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(BulletParent);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDir * bulletSpeed;

            angle += angleStep;
        }
    }
    IEnumerator ShootAtRandomIntervals()
    {
        while (true)
        {
            // �ȴ����ʱ��
            float waitTime = Random.Range(1, 5);
            yield return new WaitForSeconds(waitTime);

            // �ͷż���
            ShootAllDirection();
        }
    }
    IEnumerator ReturnToMove()
    {
        IsReturnToMove = true;
        yield return new WaitForSeconds(2.0f);
        isAttack = false;
        IsReturnToMove = false;
    }
}
