using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private JoyStickMove joyStickMove;
    private Vector3 detailMove;
    private float moveSpeed=200;
    private Animator animator;

    public float hp;
    public float maxhp;
    public bool isDead;
    public GameObject HealthBat;
    public Image HealthImage;
    public Transform footAnchor;
    public bool Invincible;
    private void Awake()
    {
        hp = maxhp;
        //��ȡ�������
        animator = GetComponent<Animator>();
        //ע��ҡ���¼�
        this.joyStickMove = FindObjectOfType<JoyStickMove>();
        this.joyStickMove.onMoveStart += this.onMoveStart;
        this.joyStickMove.onMoving += this.onMoving;
        this.joyStickMove.onMoveEnd += this.onMoveEnd;
        GamePropManager.UseDrumstick += ReplyHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDestroy()
    {
        GamePropManager.UseDrumstick -= ReplyHp;
    }

    // Update is called once per frame
    void Update()
    {
        HealthImage.fillAmount = hp / maxhp;
        if (isDead==false)
        {
            
            //Ѫ������
            //Vector3 screenPos = Camera.main.WorldToScreenPoint(footAnchor.position);
            HealthBat.transform.position = footAnchor.position;
        }
        
    }
    private void FixedUpdate()
    {
        if (isDead == false)
        {
            //���������ƶ��ͳ���
            GetComponent<Rigidbody2D>().velocity = this.detailMove * Time.deltaTime * moveSpeed;
            //this.transform.Translate(this.detailMove * Time.deltaTime * moveSpeed, Space.World);
            CheckDirection();
        }
    }
    public void Revive()
    {
        gameObject.SetActive(true);
        hp = maxhp;
        isDead = false;
        animator.SetBool("IsDead", false);
        FindObjectOfType<EnemyManager>().ClearAllEnemy();
        StartCoroutine(InvincibleTime());
    }
    public void ReplyHp()
    {
        hp = maxhp;
    }
    public void Damage(int i)
    {
        hp -= i;
        HealthImage.fillAmount = hp/maxhp;
        if(hp>0)
        {
            animator.SetTrigger("IsHurt");
        }
        else if(hp<=0)
        {
            LevelManager.Instance.ShowGameOver();
            Dead();
        }
    }
    public void Dead()
    {
        WeaponManager.Instance.StopWeapon();
        isDead= true;
        animator.SetBool("IsDead", true);
    }
    public void Hide()
    {
        //transform.GetComponent<SpriteRenderer>().color.a = 0;
        //gameObject.SetActive(false);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && Invincible == false)
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                Damage(collision.gameObject.GetComponent<Enemy>().attack);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(InvincibleShortTime());
            }
        }
    }
    //�޵�ʱ��
    IEnumerator InvincibleTime()
    {
        Invincible = true;
        yield return new WaitForSeconds(2.0f);
        Invincible = false;
    }
    //���˼��ʱ��
    IEnumerator InvincibleShortTime()
    {
        Invincible = true;
        yield return new WaitForSeconds(0.5f);
        Invincible = false;
    }
    //�ƶ��¼�
    #region
    //ҡ���¼�
    public void onMoveStart()
    {
        
    }
    public void onMoving(Vector2 vector2)
    {
        this.detailMove = new Vector3(vector2.x, vector2.y, 0);
        animator.SetBool("IsWalking", true);
    }

    public void onMoveEnd()
    {
        this.detailMove = Vector2.zero;
        animator.SetBool("IsWalking", false);
    }
    #endregion
    //�ı��ɫ����
    public void CheckDirection()
    {
        if(detailMove.x<0 && transform.localScale.x>0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if(detailMove.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
}
