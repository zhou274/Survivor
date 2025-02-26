using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject Explode;
    public int damage;
    public int Force;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(Explode, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.RockHit, Camera.main.transform.position,0.5f);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
        }
    }
    IEnumerator CountDestroy()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
