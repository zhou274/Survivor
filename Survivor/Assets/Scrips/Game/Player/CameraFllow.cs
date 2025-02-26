using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public Transform target; // 跟随的目标
    public Vector3 offset = new Vector3(0, 0, -10); // 摄像机与目标的偏移量

    void LateUpdate()
    {
        if (target != null)
        {
            // 设置摄像机的位置为目标位置 + 偏移量
            transform.position = target.position + offset;
        }
    }
}
