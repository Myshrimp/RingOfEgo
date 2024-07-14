using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data_Picker<T> : MonoBehaviour  where T : data_typeAs_tagged<T>
{
   
    public data_C<T> source;
    public int pointer;

    [Header("2º∂≤È—Ø")]
    public data_C<data_List<T>> source_deep;
    public int pointer_deep;

    public string nowUse_name = "Œ¥’“µΩ";
    public T nowUse;
    private void Update()
    {
        if (source != null)
        {
            nowUse_name = "NULL";
            find_index();
        }else if (source_deep != null)
        {
            find_index();
        }
    }
    public void find_index()
    {
        if(source.datas.Count>pointer)
        {
            setUse(source.datas[pointer]);
        }
    }
    public void find_deep_index()
    {

    }
    private void setUse(T t)
    {
        nowUse = t;
        nowUse_name = nowUse.name_;
    }
}
