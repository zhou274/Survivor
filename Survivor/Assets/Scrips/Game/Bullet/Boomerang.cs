using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float rotateSpeed;
    public Transform player;
    public bool isRenturn;
    public float moveSpeed;

    public GameObject Explode;
    public int damage;
    public int Force;
    private void Awake()
    {
        StartCoroutine(CountBack());
    }
    // Start is called before the first frame update
    void Start()
    {
        isRenturn = false;
        player= FindObjectOfType<Player>().transform;
    }
    private void LateUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        if(isRenturn==true)
        {
            Vector2 NewDic=player.position-transform.position;
            GetComponent<Rigidbody2D>().velocity = NewDic.normalized*5;
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance<0.1)
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator CountBack()
    {
        yield return new WaitForSeconds(1.0f);
        isRenturn = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(Explode, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.BoomerangHit, Camera.main.transform.position,0.3f);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
        }
    }
}
