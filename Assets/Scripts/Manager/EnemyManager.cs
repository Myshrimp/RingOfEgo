using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyManager : Singleton<EnemyManager>
{
    List<GameObject> enemyList;//敌人列表，通过列表来监视场景中的敌人，创建一个敌人时，敌人进入列表，杀死一个敌人时，移除死亡的敌人
    [SerializeField] private GameObject Player;
    [SerializeField] GameObject[] enemyPerfabs;//敌人预制体数组
    [SerializeField] public bool spawnEnemy = true;//是否生成敌人
    [SerializeField] float defaultTimeBetweenSpawns = 1f;//敌人生成间隔时间

    [SerializeField] private float sceneRadius = 10f;

    [SerializeField] private Vector2 sceneCenter = Vector2.zero;
    
    // [SerializeField] int minEnemyAmount = 3;//最小敌人数量
    // [SerializeField] int maxEnemyAmount = 5;//最大敌人数量
    [SerializeField] float minTimeBetweenSpawns = 0.1f; // 最小的敌人生成间隔时间
    [SerializeField] float defaultTimeReductionFactor = 0f; // 每1秒减少的时间间隔因子
    [SerializeField] private float timeReductionDeltaValue = 0.05f;
    [SerializeField] private float timeReductionSpawns = 10f;
    [SerializeField] private float playerSafeBoundingRadius = 3f;

    private float currentTimeBetweenSpawn;
    private float currentTimeReductionFactor;
    private float timeSinceLastReductionIncrease;//计时器
    private WaitForSeconds waitTimeBetweenSpawns;
    // int enemyAmount;//敌人数量
    
    protected override void Awake()
    {
        base.Awake();
        currentTimeBetweenSpawn = defaultTimeBetweenSpawns;
        currentTimeReductionFactor = defaultTimeReductionFactor;
        enemyList = new List<GameObject>();

        
    }


    private void Start()
    {
        SetSpawnRegion(CheckMouseViable.instance.GetRadius(), CheckMouseViable.instance.transform.position);
    }
    private IEnumerator StartSpawn()
    {
        while (spawnEnemy)
        {
            yield return StartCoroutine(nameof(RandomlySpawnCoroutine));
        }
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(nameof(StartSpawn));
    }

    IEnumerator RandomlySpawnCoroutine()
    {
        Vector2 initializePosition = GetRandomPointInCircle(sceneRadius, sceneCenter);

        while (Vector2.Distance(Player.transform.position, initializePosition) < playerSafeBoundingRadius)
        {
            initializePosition = GetRandomPointInCircle(sceneRadius, sceneCenter);
        }
        
        enemyList.Add(PoolManager.Relese(enemyPerfabs[Random.Range(0, enemyPerfabs.Length)], initializePosition));
        Debug.Log("1");

        float gameTime = TimeController.Instance.GetGameTime();
        Debug.Log("GameTime:" + gameTime);

        timeSinceLastReductionIncrease += currentTimeBetweenSpawn;
        if (timeSinceLastReductionIncrease >= timeReductionSpawns)
        {
            currentTimeReductionFactor += timeReductionDeltaValue;
            timeSinceLastReductionIncrease = 0f;
        }
        
        currentTimeBetweenSpawn = Mathf.Max(minTimeBetweenSpawns, defaultTimeBetweenSpawns - currentTimeReductionFactor);
        Debug.Log("currentTimeBetweenSpawn:" + currentTimeBetweenSpawn);
        
        yield return new WaitForSeconds(currentTimeBetweenSpawn);
    }
    
    public void RemoveFromList(GameObject enemy) => enemyList.Remove(enemy);
    
    //获得圆内随机点
    public Vector2 GetRandomPointInCircle(float radius, Vector2 center)
    {
        // 随机生成一个角度
        float angle = Random.Range(0f, Mathf.PI * 2);
    
        // 随机生成一个半径，使用平方根保证均匀分布
        float randomRadius = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;
    
        // 转换为笛卡尔坐标
        float x = randomRadius * Mathf.Cos(angle);
        float y = randomRadius * Mathf.Sin(angle);
    
        // 返回相对于给定圆心的点
        return new Vector2(x + center.x, y + center.y);
    }

    public void SetSpawnRegion(float radius,Vector2 center)
    {
        sceneCenter = center;
        sceneRadius = radius;
    }
}


