using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_InitClass : CenterBase<Center_InitClass>
{
    // Start is called before the first frame update
    public override void awake_()
    {
        base.awake_();
        new deFendWayStore().do_InitInstance();//创建非脚本实例
       // deFendWayStore.Instance.do_InitWays();//已经内置了
        Debug.Log("完成了吃书画策略");
    }
}
