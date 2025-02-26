using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.3f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject BlueRock;
    public GameObject BlueBlockManager;
    public Transform BulletParent;

    public int hp;
    public int attack;
    public bool isDead;
    public bool isHurt;

    
    private void Awake()
    {
        BlueBlockManager = FindObjectOfType<BlueBlockManager>().gameObject;
        BulletParent = FindObjectOfType<BulletManager>().transform;
    }
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
    }
    
    public virtual void Update()
    {
        Move();
    }
    //移动
    public virtual void Move()
    {
        //控制移动
        if (isDead == false)
        {
            if (player != null)
            {
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
        }
    }
    
    //受伤 死亡函数
    #region
    public virtual void OnDemage(int i,int force)
    {
        hp -= i;
        if (hp>0)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.AddForce(-direction * force);
            animator.SetTrigger("IsHurt");
        }
        if(hp<=0)
        {
            isDead = true;
            Dead();
        }
    }
    public virtual void Dead()
    {
        GetComponent<Collider2D>().enabled = false;
        LevelManager.Instance.KillCount += 1;
        isDead = true;
        animator.SetBool("IsDead", true);
    }
    public void Hide()
    {
        GameObject prop = Instantiate(BlueRock, transform.position, Quaternion.identity);
        prop.transform.SetParent(BlueBlockManager.transform);
        Destroy(gameObject);
    }
    #endregion
}
