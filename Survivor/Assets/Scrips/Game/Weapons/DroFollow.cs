using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroFollow : MonoBehaviour
{
    public Transform target; // 绑定主角的Transform 
    public float followSpeed = 3f;
    public Vector2 offset = new Vector2(1, 1); // 无人机相对主角的偏移量 
    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        // 计算目标位置（带偏移）
        Vector2 targetPos = (Vector2)target.position + offset;
        // 平滑移动 
        transform.position = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
