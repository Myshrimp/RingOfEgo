using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBase<T> : GameBehavior where T :GameBehavior 
{
    public static T Instance;
    public override void awake_pre()
    {
        base.awake_pre();
        if (Instance == null) Instance = this as T;
        else
        {
            Debug.Log("�������ظ�����,�ر���֡�¼�");
           isAdmittedUpdate= false;
        }
    }
}
