using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private GameObject player; 
    private Transform playerTransform;// 玩家位置
    [SerializeField] public float chaseRadius = 10f; // 追击半径
    [SerializeField] public float patrolRadius = 5f; // 巡逻半径
    [SerializeField] public float patrolSpeed = 2f; // 巡逻速度
    [SerializeField] public float chaseSpeed = 4f; // 追击速度
    public Vector2 patrolCenter; // 巡逻中心

    private Rigidbody2D rb;
    private Vector2 patrolPoint;
    private bool isChasing = false;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Camp0");
        
        if (player != null)
            playerTransform = player.transform;
        
        patrolCenter = transform.position; // 初始化巡逻中心为敌人初始位置
        SetRandomPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            // 在追击半径内，追击玩家
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            // 不在追击半径内，巡逻
            isChasing = false;
            Patrol();
        }
    }

    void SetRandomPatrolPoint()
    {
        patrolPoint = EnemyManager.Instance.GetRandomPointInCircle(patrolRadius, patrolCenter);
        Debug.Log(patrolPoint);
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoint) < 0.1f)
        {
            SetRandomPatrolPoint();
        }
        else
        {
            MoveTowards(patrolPoint, patrolSpeed);
        }
    }

    void ChasePlayer()
    {
        MoveTowards(playerTransform.position, chaseSpeed);
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - rb.position).normalized;
        rb.velocity = direction * speed;
    }
}
    
