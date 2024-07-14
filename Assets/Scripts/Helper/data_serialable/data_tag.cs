using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class data_tag 
{
    [SerializeField]
    public int id=-1;
    [SerializeField]
    public string name_;
    [SerializeField]
    public string des_;
}
//game object包
[Serializable]
public class data_typeAsGameObject : data_typeAs_tagged<GameObject >
{

}
[Serializable]
public class data_List_GameObject : data_List<GameObject >
{

}


//两个float包
[Serializable ]
public class data_typeAsFloat:data_typeAs_tagged <float>
{
    
}
[Serializable]
public class data_List_float:data_List<float>
{

}
[Serializable ]
public class data_List<K>:data_typeAs_tagged<K>
{
    [Header("数据存储")]
    [SerializeField]
    public List<data_typeAs_tagged<K>> datas;
}
[Serializable ]
public class data_typeAs_tagged<T>: data_typeAs<T>
{
    [SerializeField]
    public data_tag tag;
}


public class data_typeAs<T> 
{
    [SerializeField ]
    public string name_;
    [SerializeField ]
    public T value;
    private T value_origin;
    private bool hasSetOri;
    public data_typeAs()
    {
        do_Save();
       
    }
    public T do_Save()
    {
        value_origin = value;
     //   Debug.Log("储存值");
        return value;
    }
    public T getValue()
    {
        return value;
    }
    public void  setValue(T t)
    {
        value = t;
    }
    public T do_Back()
    {
        value = value_origin;
        return value;
    }
}