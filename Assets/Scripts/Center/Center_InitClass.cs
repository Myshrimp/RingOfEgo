using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_InitClass : CenterBase<Center_InitClass>
{
    // Start is called before the first frame update
    public override void awake_()
    {
        base.awake_();
        new deFendWayStore().do_InitInstance();//�����ǽű�ʵ��
       // deFendWayStore.Instance.do_InitWays();//�Ѿ�������
        Debug.Log("����˳��黭����");
    }
}
