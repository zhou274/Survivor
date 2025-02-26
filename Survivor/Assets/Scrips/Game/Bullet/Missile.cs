using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{

    public float detectRadius = 5f;
    public LayerMask enemyLayer; // 在Inspector中设置敌人层级 
    public GameObject target;
    public bool isStart;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null &&isStart==true)
        {

            direction=(target.transform.position-transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction*moveSpeed;
            //控制旋转
            Vector2 directio = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directio.y, directio.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
            transform.rotation = targetRotation;
        }
        else if (target==null && isStart == true)
        {
            target = GetNearestEnemy();
            if(target==null)
            {
                Instantiate(Explode, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator CheckEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        target=GetNearestEnemy();
        isStart = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    private GameObject GetNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRadius, enemyLayer);
        GameObject closest = null;
        float minSqrDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            Vector2 direction = hit.transform.position - transform.position;
            float sqrDistance = direction.sqrMagnitude;

            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closest = hit.gameObject;
            }
        }
        return closest;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(Explode, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().OnDemage(damage, Force);
            SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.Boom, Camera.main.transform.position,1);
            Destroy(gameObject);
        }
    }
}
