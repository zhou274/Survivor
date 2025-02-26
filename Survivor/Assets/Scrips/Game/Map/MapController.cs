using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject chunkPrefab;  // ����Ԥ����
    public Transform player;        // ���λ��
    //���ص�ǰ����ש�鸽���ĵ�ͼ
    public int loadDistance = 3;    // ���ؾ��루���鵥λ��
    public GameObject MapParent;

    private Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();
    private Vector2Int currentPlayerChunk=new Vector2Int(0,1);

    void Update()
    {
        // ������ҵ�ǰ������������
        Vector2Int playerChunk = new Vector2Int(
            Mathf.FloorToInt(player.position.x / 10.24f),
            Mathf.FloorToInt(player.position.y / 10.24f)
        );

        if (playerChunk != currentPlayerChunk)
        {
            currentPlayerChunk = playerChunk;
            UpdateChunks();
        }
    }

    void UpdateChunks()
    {
        // ж�س�����Χ������
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunkPos in loadedChunks.Keys)
        {
            if (Vector2Int.Distance(chunkPos, currentPlayerChunk) > loadDistance)
            {
                Destroy(loadedChunks[chunkPos]); // �����������
                chunksToRemove.Add(chunkPos);    // ��¼��Ҫ�Ƴ�����������
            }
        }

        // ����������
        for (int x = -loadDistance; x <= loadDistance; x++)
        {
            for (int y = -loadDistance; y <= loadDistance; y++)
            {
                Vector2Int chunkPos = currentPlayerChunk + new Vector2Int(x, y);
                if (!loadedChunks.ContainsKey(chunkPos))
                {
                    GameObject chunkObj = Instantiate(chunkPrefab, new Vector3(chunkPos.x * 10.24f, chunkPos.y * 10.24f, 0), Quaternion.identity);
                    chunkObj.transform.SetParent(MapParent.transform);
                    Chunk chunk = chunkObj.GetComponent<Chunk>();
                    //chunk.GenerateTerrain(); // ���ɵ���
                    loadedChunks.Add(chunkPos, chunk);
                }
            }
        }
    }
}
