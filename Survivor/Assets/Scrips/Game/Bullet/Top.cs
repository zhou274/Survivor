using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    public Transform TopManager;  // 拖拽绑定主角的Transform 
    public float RoteSpeed = 30f;  // 控制旋转速度 
    public int damage;
    public int Force;
    
    //public GameObject Explode;
    // Start is called before the first frame update
    private void Awake()
    {
        

    }
    void Start()
    {
        TopManager=transform.parent;
        Debug.Log(TopManager.name);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(TopManager.position, Vector3.forward, RoteSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Instantiate(Explode, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
            //Destroy(gameObject);
        }
    }
}
