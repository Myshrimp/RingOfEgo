using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] vFXPools;//视觉特效池数组
    [SerializeField] Pool[] enemyPools;//敌人池数组

    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        //初始化
        Initialize(enemyPools);
        Initialize(vFXPools);
    }

    #if UNITY_EDITOR
    private void OnDestroy()
    {
        //尺寸检查
        CheckPoolSize(enemyPools);
        CheckPoolSize(vFXPools);
    }
    #endif

    void CheckPoolSize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            if(pool.RunTimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Pool :{0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.RunTimeSize,
                    pool.Size));
            }
        }

    }

    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            #if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools! Prefab:" + pool.Prefab.name);
                continue;
            }
            #endif
            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;

            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// <para>Return a specified<paramref name="prefab"/>gameObject in the pool</para>
    /// <para>根据传入的<paramref name="prefab"/>参数返回对象池中预备好的游戏对象</para>
    /// </summary>
    /// <param name="prefab"></param>
    /// <para>Specified gameObject prefab.</para>
    /// <para>指定游戏对象的预制体</para>
    /// <returns>
    /// <para>Prepared gameObject in the pool</para>
    /// <para>对象池中预备好的游戏对象</para>
    /// </returns>
    public static GameObject Relese(GameObject prefab)
    {
        #if UNITY_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PrepareObject();
    }
    /// <summary>
    /// <para>Return a specified prepared gameObject in the pool at specified position</para>
    /// <para>根据传入的prefab参数,在position位置释放对象池中预备好的游戏对象</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>Specified gameObject prefab.</para>
    /// <para>指定游戏对象的预制体</para>
    /// </param>
    /// <param name="position">
    /// <para>Specified release position</para>
    /// <para>指定释放的位置</para>
    /// </param>
    /// <returns>
    /// <para>Prepared gameObject in the pool</para>
    /// <para>对象池中预备好的游戏对象</para>
    /// </returns>
    public static GameObject Relese(GameObject prefab,Vector3 position)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {

            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PrepareObject(position);
    }

    public static GameObject Relese(GameObject prefab, Vector3 position,Quaternion rotation)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {

            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PrepareObject(position,rotation);
    }

    public static GameObject Relese(GameObject prefab,Vector3 position, Quaternion rotation,Vector3 localScale)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {

            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PrepareObject(position,rotation,localScale);
    }
    
}
