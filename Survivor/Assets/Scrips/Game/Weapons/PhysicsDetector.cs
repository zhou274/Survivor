using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDetector : MonoBehaviour
{
    public float detectRadius = 5f;
    public LayerMask enemyLayer; // 在Inspector中设置敌人层级 
    public LayerMask PropLayer;
    public GameObject target;
    private void Awake()
    {
        GamePropManager.UseMagnet += GetNearBlueBlock;
    }
    private void OnDestroy()
    {
        GamePropManager.UseMagnet -= GetNearBlueBlock;
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
    public void GetNearBlueBlock()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRadius, PropLayer);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<BlueRock>().isByMagnet = true;
        }
    }
    void Update()
    {
        target = GetNearestEnemy();
        if(Input.GetKeyDown(KeyCode.K))
        {
            GetNearBlueBlock();
        }
        //if (target) Debug.Log("锁定目标：" + target.name);
    }

    // 可视化检测范围 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
