using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject chunkPrefab;  // 区块预制体
    public Transform player;        // 玩家位置
    //加载当前所在砖块附近的地图
    public int loadDistance = 3;    // 加载距离（区块单位）
    public GameObject MapParent;

    private Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();
    private Vector2Int currentPlayerChunk=new Vector2Int(0,1);

    void Update()
    {
        // 计算玩家当前所在区块坐标
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
        // 卸载超出范围的区块
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunkPos in loadedChunks.Keys)
        {
            if (Vector2Int.Distance(chunkPos, currentPlayerChunk) > loadDistance)
            {
                Destroy(loadedChunks[chunkPos]); // 销毁区块对象
                chunksToRemove.Add(chunkPos);    // 记录需要移除的区块坐标
            }
        }

        // 加载新区块
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
                    //chunk.GenerateTerrain(); // 生成地形
                    loadedChunks.Add(chunkPos, chunk);
                }
            }
        }
    }
}
