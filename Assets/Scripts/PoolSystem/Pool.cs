using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///对象池
///<summary>

[System.Serializable] public class Pool
{

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    public int Size => size;

    public int RunTimeSize => queue.Count;

    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;

    Queue<GameObject> queue;
    Transform parent;

    public void Initialize(Transform parent)//初始化
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for(int i = 0;i < size;i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()//先往对象池里面放对象
    {
        var copy = GameObject.Instantiate(prefab,parent);

        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()//找出队列中可以使用的预备对象返回
    {
        GameObject availObject = null;
        
        if(queue.Count > 0 && !queue.Peek().activeSelf)//Peek返回队列第一个元素
        {
            availObject = queue.Dequeue();
        }
        else
        {
            availObject = Copy();
        }

        queue.Enqueue(availObject);//让任务对象提前返回到池中

        return availObject;
    }

    //一共四个重载。我们可能需要这个对象出现在指定的位置，并旋转一定角度或者缩放一定比例
    public GameObject PrepareObject()//启用可用对象
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position)//启用可用对象
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation)//启用可用对象
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        prepareObject.transform.rotation = rotation;

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation, Vector3 localScale)//启用可用对象
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        prepareObject.transform.rotation = rotation;
        prepareObject.transform.localScale = localScale;
        return prepareObject;
    }
}
