using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueRock : MonoBehaviour
{
    public Transform player;
    public bool isByMagnet;
    public int moveSpeed = 2;
    private void Awake()
    {
        isByMagnet = false;
        player = FindObjectOfType<Player>().transform;
    }
    public void Update()
    {
        if(isByMagnet==true && player!=null)
        {
            Vector3 direction = player.position - transform.position;
            GetComponent<Rigidbody2D>().AddForce(direction.normalized * moveSpeed, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelManager.Instance.CurrentExperience += 1;
            Destroy(gameObject);
        }
    }

}
