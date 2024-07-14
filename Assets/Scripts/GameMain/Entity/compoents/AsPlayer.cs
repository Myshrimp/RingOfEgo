using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsPlayer : CenterBase<AsPlayer>
{
   
    public  Entity player;
    public Vector2 mousePosition;
    public override void update_()
    {
        base.update_();
        mousePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public bool isNowUseSinglePlayerAndGetState = true;
    public void setInitPlayerEntity()
    {

    }
   public void doQuick_gameStart()
    {
        player.sT("god_gameStart");
    }
    public void doQuick_dieByRing()
    {
        player.sT("god_dieByRing");
    }

}
