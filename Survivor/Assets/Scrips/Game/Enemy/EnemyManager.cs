using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] distantEnemy;
    public GameObject[] boss;
    public GameObject Weilan;
    public GameObject player;

    //普通敌人生成
    public float spawnRadius = 10;
    private float Spawninterval = 10f;
    private float SpawnNumber = 20;
    //远程敌人
    public float distantpawnRadius = 10;
    private float distantSpawninterval = 10f;
    private float distantSpawnNumber = 30;
    public static Action ChangeSpawn;
    public bool IsDistantStart=false;
    public GameObject RemindPanel;
    public float RemindTime;
    public float BossTime;
    public bool isBossAlive;
    public GameObject remindBoss;
    // Start is called before the first frame update
    void Awake()
    {
        ChangeSpawn += ImproveProgress;
        GamePropManager.UseBomb += ClearAllEnemy;
    }
    void Start()
    {
        ImproveProgress();
        StartCoroutine("CreateEnemy");
    }
    void OnDestroy()
    {
        ChangeSpawn-= ImproveProgress;
        GamePropManager.UseBomb -= ClearAllEnemy;
    }
    // Update is called once per frame
    void Update()
    {
        //if(IsDistantStart==false && LevelManager.Instance.Level>7 && isBossAlive == false)
        //{
        //    StartCoroutine("CreateDistantEnemy");
        //    IsDistantStart = true;
        //}
        if(LevelManager.Instance.isOver==false && isBossAlive==false)
        {
            RemindTime += Time.deltaTime;
            if(RemindTime>=43)
            {
                StartCoroutine(SetRemind());
                RemindTime = 0;
            }
        }
        if(LevelManager.Instance.isOver == false && isBossAlive == false)
        {
            BossTime += Time.deltaTime;
            if(BossTime>=60)
            {
                StartCoroutine(SetRemindBoss());
                BossTime = 0;
            }
        }
        
    }
    public void ImproveProgress()
    {
        SpawnNumber = 30 + (LevelManager.Instance.Level / 5) * 10;
        if(Spawninterval<=3)
        {
            Spawninterval = 3;
        }
        else
        {
            Spawninterval = 8 - (LevelManager.Instance.Level / 5);
        }
        distantSpawnNumber = 15 + (LevelManager.Instance.Level / 5 - 1) * 5;
        if(distantSpawninterval<=5)
        {
            distantSpawnNumber = 5;
        }
        else
        {
            distantSpawninterval = 12 - (LevelManager.Instance.Level / 5);
        }
    }
    public void ClearAllEnemy()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<Enemy>().Dead();
        }
    }
    public void StopCreate()
    {
        StopCoroutine("CreateEnemy");
        StopCoroutine("CreateDistantEnemy");
    }
    public void StartCreate()
    {
        StartCoroutine("CreateEnemy");
        StartCoroutine("CreateDistantEnemy");
    }
    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (true) 
        {
            for(int i=0;i< SpawnNumber; i++)
            {
                int index = UnityEngine.Random.Range(0, enemy.Length);
                Vector2 randomPos = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 spawnPosition = player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
                GameObject Enemyc= Instantiate(enemy[index], spawnPosition, Quaternion.identity);
                Enemyc.transform.GetComponent<Enemy>().hp = 10 + (LevelManager.Instance.Level / 3) * 20;
                Enemyc.transform.SetParent(transform);
            }
            yield return new WaitForSeconds(Spawninterval);
        }
        
    }
    public void CreateBoss()
    {
        ClearAllEnemy();
        StopCreate();
        int index = UnityEngine.Random.Range(0, boss.Length);
        GameObject Boss = Instantiate(boss[index], new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z), Quaternion.identity);
        isBossAlive = true;
        Boss.transform.GetComponent<Enemy>().hp = 500 + (LevelManager.Instance.Level / 5) * 500;
        Instantiate(Weilan, player.transform.position, Quaternion.identity);
    }
    public void CreateLotEnemy()
    {
        for (int i = 0; i < SpawnNumber+(LevelManager.Instance.Level/3)*40; i++)
        {
            int index = UnityEngine.Random.Range(0, enemy.Length);
            Vector2 randomPos = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
            GameObject Enemyc = Instantiate(enemy[index], spawnPosition, Quaternion.identity);
            Enemyc.transform.GetComponent<Enemy>().hp = 10 + (LevelManager.Instance.Level / 3) * 20;
            Enemyc.transform.SetParent(transform);
        }
    }
    IEnumerator CreateDistantEnemy()
    {
        while(true)
        {
            for(int i=0;i<distantSpawnNumber;i++)
            {
                int index = Random.Range(0, distantEnemy.Length);
                Vector2 randomPos = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 spawnPosition = player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
                GameObject Enemyc = Instantiate(distantEnemy[index], spawnPosition, Quaternion.identity);
                Enemyc.transform.GetComponent<Enemy>().hp = 10 + (LevelManager.Instance.Level / 3) * 20;
                Enemyc.transform.SetParent(transform);
            }
            yield return new WaitForSeconds(distantSpawnNumber);
        }
    }
    IEnumerator SetRemind()
    {
        RemindPanel.SetActive(true);
        SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.Remind, Camera.main.transform.position);
        yield return new WaitForSeconds(3.0f);
        RemindPanel.SetActive(false);
        CreateLotEnemy();
    }
    IEnumerator SetRemindBoss()
    {
        remindBoss.SetActive(true);
        SoundManager.instance.PlaySound(SoundManager.instance.audioClipRefsSO.Remind, Camera.main.transform.position);
        yield return new WaitForSeconds(3.0f);
        remindBoss.SetActive(false);
        CreateBoss();
    }
}
