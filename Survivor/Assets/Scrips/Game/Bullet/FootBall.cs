using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBall:MonoBehaviour
{
    public GameObject Explode;
    public int damage;
    public int Force;
    public float moveSpeed = 5;

    public int maxBounces = 3; // 最大弹射次数 
    private int currentBounces;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HandleEnemyBounce(Collider2D collision)
    {
        currentBounces++;
        if (currentBounces >= maxBounces)
        {
            Destroy(gameObject);
            return;
        }
        // 寻找下一个敌人目标 
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5f, LayerMask.GetMask("Enemy"));

        if (enemies.Length > 0)
        {
            // 随机选择或找最近敌人 
            Collider2D nextTarget = enemies[Random.Range(0, enemies.Length)];
            Vector2 newDir = (nextTarget.transform.position - transform.position).normalized;
            rb.velocity = newDir * moveSpeed;
            transform.right = rb.velocity.normalized;  
        }
        else // 无目标时直线飞行 
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        Instantiate(Explode, transform.position, Quaternion.identity);
    //        HandleEnemyBounce(collision);
    //        //Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(Explode, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.FootBallHit, Camera.main.transform.position,0.5f);
            HandleEnemyBounce(collision);
            //Destroy(gameObject);
        }
    }
}
