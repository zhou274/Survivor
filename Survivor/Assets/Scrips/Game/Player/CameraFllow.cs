using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public Transform target; // �����Ŀ��
    public Vector3 offset = new Vector3(0, 0, -10); // �������Ŀ���ƫ����

    void LateUpdate()
    {
        if (target != null)
        {
            // �����������λ��ΪĿ��λ�� + ƫ����
            transform.position = target.position + offset;
        }
    }
}
