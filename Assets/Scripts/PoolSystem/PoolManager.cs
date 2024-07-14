using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] vFXPools;//�Ӿ���Ч������
    [SerializeField] Pool[] enemyPools;//���˳�����

    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        //��ʼ��
        Initialize(enemyPools);
        Initialize(vFXPools);
    }

    #if UNITY_EDITOR
    private void OnDestroy()
    {
        //�ߴ���
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
    /// <para>���ݴ����<paramref name="prefab"/>�������ض������Ԥ���õ���Ϸ����</para>
    /// </summary>
    /// <param name="prefab"></param>
    /// <para>Specified gameObject prefab.</para>
    /// <para>ָ����Ϸ�����Ԥ����</para>
    /// <returns>
    /// <para>Prepared gameObject in the pool</para>
    /// <para>�������Ԥ���õ���Ϸ����</para>
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
    /// <para>���ݴ����prefab����,��positionλ���ͷŶ������Ԥ���õ���Ϸ����</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>Specified gameObject prefab.</para>
    /// <para>ָ����Ϸ�����Ԥ����</para>
    /// </param>
    /// <param name="position">
    /// <para>Specified release position</para>
    /// <para>ָ���ͷŵ�λ��</para>
    /// </param>
    /// <returns>
    /// <para>Prepared gameObject in the pool</para>
    /// <para>�������Ԥ���õ���Ϸ����</para>
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
