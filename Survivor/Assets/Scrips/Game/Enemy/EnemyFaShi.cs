using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaShi : Enemy
{
    public GameObject GreenBullet;
    private bool isAttack;
    public override void Start()
    {
        base.Start();
        StartCoroutine(CreateBullet());
    }
    public override void Update()
    {
        if(isAttack==false)
        {
            rb.simulated = true;
            Move();
        }
        else if(isAttack==true)
        {
            rb.simulated = false;
        }
    }
    IEnumerator CreateBullet()
    {
        while (true) 
        {
            yield return new WaitForSeconds(2.5f);
            Collider2D Player = Physics2D.OverlapCircle(transform.position, 3f, LayerMask.GetMask("Player"));
            if(Player!=null)
            {
                animator.SetBool("IsAttack", true);
                
            }
        }
    }
    public void CreateBall()
    {
        GameObject bullet = Instantiate(GreenBullet, transform.position, Quaternion.identity);
        bullet.transform.SetParent(BulletParent);
        bullet.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * bullet.GetComponent<GreenBall>().moveSpeed, ForceMode2D.Impulse);
    }
    public void Attack()
    {
        isAttack=true;
    }
    public void EndAttack()
    {
        isAttack = false;
        animator.SetBool("IsAttack", false);
    }
}
