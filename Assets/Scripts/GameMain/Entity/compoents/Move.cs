using System.Collections;
using UnityEngine;
public class Move : ComAddition
{
    [HideInInspector]
    public float speed=30;
    public float speedOr=3;
    public float fx, fy, faceTo;
    public Vector2 fv;

    public bool isSliding;
    private float banSlid;
    public float time_slid;

    public bool isJumping;
    public bool isConHard;

    private State current_state;
    public float detect_radius = 10f;
    public float attack_radius = 1f;
    public override void awake_pre()
    {
        base.awake_pre();
        willUseRigidBody = true;
        speed = speedOr;

        current_state = State.Patrol;
    }
    // Start is called before the first frame update
    private float timer = 0f;
    public float patrol_speed = 0.2f;
    public float chase_speed = 0.5f;
    public float attack_frequency = 5f;
    private bool patrol_movable = true;
    public override void beActive()
    {
        base.beActive();
        module_SlidStart();
        module_Run();
        module_SlidIn();

        if(CompareTag("Camp1")) //if it's enemy
        {
            GameObject player = GameObject.FindWithTag("Camp0");//reference of player
            float distance = (player.transform.position - this.transform.position).magnitude;
            SwitchState(distance);

            if(current_state == State.Patrol)
            {
                
                if(transform.position.x - player.transform.position.x < 0)
                {
                    fx = patrol_speed;
                    fy = 0;
                }
                if (transform.position.x - player.transform.position.x > 0)
                {
                    fx = -patrol_speed;
                    fy = 0;
                }

                if(!patrol_movable)
                {
                    fx = 0;
                    fy = 0;
                }

            }

            if(current_state == State.Chase)
            {
                Vector2 dir=player.transform.position - this.transform .position;
                dir = dir.normalized;
                fx = dir.x * chase_speed;
                fy = dir.y * chase_speed;
            }

            if(current_state == State.Attack)
            {
                fx = 0;
                fy = 0;
                timer += Time.deltaTime;
                if(timer>=1/attack_frequency)
                {
                    //entity.sT("attLayer");
                    entity.myWeapon.module_useWeapon(true);
                    timer = 0;
                }             
            }
        }
    }

    private void SwitchState(float distance)
    {
        if (distance < detect_radius && distance > attack_radius)
        {
            current_state = State.Chase;
            StopCoroutine(nameof(EnterPatrolState));
        }
        if (distance > detect_radius)
        {
            current_state = State.Patrol;
            StartCoroutine(nameof(EnterPatrolState));
        }
        if (distance < attack_radius)
        {
            current_state = State.Attack;
            StopCoroutine(nameof(EnterPatrolState));
        }
    }

    public void module_SlidStart()
    {
        banSlid -= Time.deltaTime;
        if (time_slid > 0) return;
        if (keyDown(KeyCode.K))
        {
            startSlid();
        }
    }
    public void module_Run()
    {
        if (isConHard) return;//被硬控了
        if (isCanUseKey()) {

            fx = Input.GetAxis("Horizontal");
            fy = Input.GetAxis("Vertical");
          //  Debug.Log("使用fxfy");
        }else
        {
            Debug.Log("出事了，不能动");
        }
            fv = speed * new Vector2(fx, fy);
        
       
           
             if (entity.isPlayer)
            {
                //wanjia
                Debug.Log(AsPlayer.Instance.mousePosition.x - transform.position.x);
                transform.rotation = Quaternion.Euler(0, faceTo = AsPlayer.Instance.mousePosition.x-transform .position .x > 0 ? 0 : 180, 0);//方向判断
            }else if(fx!=0)
            {
                transform.rotation = Quaternion.Euler(0, faceTo = fx > 0 ? 0 : 180, 0);//方向判断
            }
        
        bool isMoving = fx != 0 || fy != 0;
        if (isMoving)
        {
            rd.velocity = fv;
            entity.sB_set("isMoving", true);//有非零，移动
            muduleAddition_speedUp();
        }else
        {
           // Debug.Log("速度归零");
            entity.sB_set("isMoving", false);
            rd.velocity = Vector2.zero;
        }


    }
    private void muduleAddition_speedUp()
    {
        speed = speedOr;
        if (keyOn(KeyCode.Space)){
            speed *= 2;
        }else
        {

        }
        entity.sF("runScale", speed / speedOr);
    }
    public void module_SlidIn()
    {
        if (isConHard)
        {
            float t = entity.sF("slidTime");

            if (t > 0) {
                Debug.Log("滑行吧");
               fv = speed * 2.5f  * (fx != 0 || fy != 0 ? new Vector3(fx, fy, 0).normalized : new Vector3(Mathf.Cos(faceTo * Mathf.Deg2Rad), 0, 0).normalized);
                rd.velocity = fv;
             
            }
            else
            {
                isSliding = false;
               isConHard = false;
                banSlid = 1.5f;
            }
        }
    }
    public void startSlid()
    {
        if (entity.sB_get("isUsingSingleAction"))  return; 
        if (isSliding) return;
        //滑行
        entity.sF("slidTime", 2);
        entity.sT("slidStart");
        isSliding = true;
        isConHard = true;
    }
    public void endSlid()
    {
        isSliding = false;
        isConHard = false;
        banSlid = 1;
    }

    public enum State
    {
        Patrol,Chase,Attack
    }

    public float patrol_pause_time = 1f;
    IEnumerator EnterPatrolState()
    {
        while (true)
        {
            yield return new WaitForSeconds(patrol_pause_time);
            patrol_movable = !patrol_movable;
        }
    }
}
