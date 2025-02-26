using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject BlueBlock;
    public GameObject BlueBlockManager;
    private void Awake()
    {
        BlueBlockManager = FindObjectOfType<BlueBlockManager>().gameObject;
    }
    private void Start()
    {
        for(int i=0;i<10;i++)
        {
            CreateBlue();
        }
    }
    public void CreateBlue()
    {
        int x = Random.Range((int)transform.position.x - 10, (int)transform.position.x + 10);
        int y = Random.Range((int)transform.position.y - 10, (int)transform.position.y + 10);
        GameObject prop= Instantiate(BlueBlock, new Vector3(x, y, 0), Quaternion.identity);
        prop.transform.SetParent(BlueBlockManager.transform);
    }
}
