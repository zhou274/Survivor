using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Explode;
    public int damage;
    public int Force;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(Explode,transform.position,Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage,Force);
            Destroy(gameObject);
        }
    }
}
