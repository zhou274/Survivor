using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroFollow : MonoBehaviour
{
    public Transform target; // �����ǵ�Transform 
    public float followSpeed = 3f;
    public Vector2 offset = new Vector2(1, 1); // ���˻�������ǵ�ƫ���� 
    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        // ����Ŀ��λ�ã���ƫ�ƣ�
        Vector2 targetPos = (Vector2)target.position + offset;
        // ƽ���ƶ� 
        transform.position = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
