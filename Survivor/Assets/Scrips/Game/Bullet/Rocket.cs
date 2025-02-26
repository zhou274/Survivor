using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket:MonoBehaviour
{
    public GameObject Explode;
    public int BaseDamage;
    public int Force;
    public float moveSpeed = 5;
    public float explosionRadius = 0.7f;
    public float explpdeScale=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Exploded()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D hit in hits)
        {
            if(hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                float Distance = Vector2.Distance(hit.transform.position, transform.position);
                //int damage = (int)(BaseDamage * (1 - Distance / explosionRadius));
                hit.GetComponent<Enemy>().OnDemage(BaseDamage, Force);
            }
        }
        SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.Boom, Camera.main.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject boom= Instantiate(Explode, transform.position, Quaternion.identity);
            boom.transform.localScale = boom.transform.localScale * explpdeScale;
            Exploded();
            //collision.gameObject.GetComponent<Enemy>().OnDemage(BaseDamage, Force);
            Destroy(gameObject);
        }
    }
}
