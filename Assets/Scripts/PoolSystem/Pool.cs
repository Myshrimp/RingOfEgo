using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///�����
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

    public void Initialize(Transform parent)//��ʼ��
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for(int i = 0;i < size;i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()//�������������Ŷ���
    {
        var copy = GameObject.Instantiate(prefab,parent);

        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()//�ҳ������п���ʹ�õ�Ԥ�����󷵻�
    {
        GameObject availObject = null;
        
        if(queue.Count > 0 && !queue.Peek().activeSelf)//Peek���ض��е�һ��Ԫ��
        {
            availObject = queue.Dequeue();
        }
        else
        {
            availObject = Copy();
        }

        queue.Enqueue(availObject);//�����������ǰ���ص�����

        return availObject;
    }

    //һ���ĸ����ء����ǿ�����Ҫ������������ָ����λ�ã�����תһ���ǶȻ�������һ������
    public GameObject PrepareObject()//���ÿ��ö���
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position)//���ÿ��ö���
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation)//���ÿ��ö���
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        prepareObject.transform.rotation = rotation;

        return prepareObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation, Vector3 localScale)//���ÿ��ö���
    {
        GameObject prepareObject = AvailableObject();

        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        prepareObject.transform.rotation = rotation;
        prepareObject.transform.localScale = localScale;
        return prepareObject;
    }
}
