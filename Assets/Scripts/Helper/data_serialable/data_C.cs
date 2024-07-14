using EV.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data_C<T> : MonoBehaviour
{
    public T nowUse;
    [SerializeField]
    protected  int pointer;
    private int pointer_memory;
    [SerializeField]
    protected bool isUpdating;
    [SerializeField]
    public List<T> datas;
    // Start is called before the first frame update
    public virtual  void Start()
    {
        
    }

    // Update is called once per frame
    public virtual   void Update()
    {
        if (isUpdating)
        {
            if (pointer != pointer_memory)
            {
                pointer_memory = pointer;
                findNowUseByPointer();
            }
        }
    }
    public T nowUse_()
    {
        return nowUse;
    }
    public T findNowUseByPointer()
    {
        if(datas!=null&&datas.Count>0)
        nowUse = findByIndex(pointer);
        return nowUse;
    }
    public T findByIndex(int i)
    {

        if (datas.Count > i)
        {
           return (datas[i]);
          
        }
        Debug.LogWarning ("无返回值 以index");
        return default;
    }
  
}
