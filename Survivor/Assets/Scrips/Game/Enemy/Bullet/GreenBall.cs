using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : MonoBehaviour
{
    public GameObject Explode;
    public int damage;
    public int Force;
    public float moveSpeed = 5;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>3)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            //Instantiate(Explode, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
