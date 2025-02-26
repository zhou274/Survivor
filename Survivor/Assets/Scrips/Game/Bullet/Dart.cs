using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Bullet
{
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
        if (collision.gameObject.tag=="Enemy")
        {
            Instantiate(Explode, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.Boom, Camera.main.transform.position, 1);
            //Debug.Log($"Collision with: {collision.gameObject.name} - Frame: {Time.frameCount}");
            Destroy(gameObject);
        }
    }
}
