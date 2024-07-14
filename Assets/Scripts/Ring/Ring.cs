using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.Experimental.Rendering.Universal;
public class Ring : MonoBehaviour
{
    public static Ring instance;
    [SerializeField] CircleCollider2D outerCollider;
    [SerializeField] CircleCollider2D innerCollider;
    [SerializeField] Material RingMaterial;
    [SerializeField] int amount_of_player_bounds = 4;
    [SerializeField] GameObject player;
    [SerializeField] float push_back_force = 10f;
    [SerializeField] LayerMask playerBoundLayer;
    [Tooltip("The speed of ring's scaling")]
    [SerializeField] public EdgeCollider2D edge;
    [SerializeField] DistanceJoint2D joint;
    [SerializeField] CircleFill circlefill;
    [SerializeField] GameObject bound;
    [Header("Collect lights")]
    [SerializeField] GameObject[] AllLights;
    //private GameObject[] lights;
    public int LightsIgnited = 0;

    [Header("")]
    public float default_scale_speed = 0.05f;

    [ReadOnly(true)]
    public float scale_speed = 0.05f;

    [Header("Edge Collider")]
    public float default_collider_radius = 3.7f;
    public float radius = 1f; // 圆形碰撞体的半径
    public int LeastSegments = 32;
    public int maxSegments = 600;
    [ReadOnly(true)]
    public int numSegments = 32; // 圆形碰撞体的线段数量
    private EdgeCollider2D edgeCollider;

    [Header("Ring Size")]
    [Tooltip("Distance between outer and inner circles")]
    public float RingGap = 1f; //Distance between outer and inner circles
    public float defaultOuterRadius = 0f;
    private float defaultInnerRadius = 0f;



    private float original_radius = 0f;
    private void Awake()
    {
        instance = this;
        edgeCollider = GetComponent<EdgeCollider2D>();
        original_radius = Vector2.Distance(bound.transform.position, transform.position);
    }
    private void Start()
    {
        RingMaterial.SetFloat("_OuterRadius", defaultOuterRadius);
        RingMaterial.SetFloat("_InnerRadius",defaultOuterRadius-RingGap);
        defaultInnerRadius = defaultOuterRadius - RingGap;
        radius = default_collider_radius;
        scale_speed = -default_scale_speed;
       
        DrawCircle();
        UpdateBounds();        
    }
    private void OnDisable()
    {
        RingMaterial.SetFloat("_OuterRadius", defaultOuterRadius);
        RingMaterial.SetFloat("_InnerRadius", defaultOuterRadius-RingGap);
    }


    private bool shrinking = true;
    private void Update()
    {
        /*
         in main loop,you should call UpdateRing in Update function

        for example:
        UpdateRing();
        you can change scale speed by calling:
        AddExtraScaleSpeed(float);(positive float for speeding up and negative for slowing down)
         */
        UpdateRing();
        LightsIgnited = 0;
        foreach (var light in AllLights)
        {
            if((light.transform.position - this.transform.position).magnitude <= GetRadius())
            {
                LightsIgnited += 1;
                light.SetActive(true);
            }
            else
            {
                light.SetActive(false);
            }
            //print("ignited:"+LightsIgnited);
            circlefill.SetFillAmount(LightsIgnited / 12f);
        }
        
        scale_speed = Mathf.Lerp(scale_speed, -default_scale_speed, 0.01f);
    }



    public void ForceRingScale() //might be useless,just for test
    {
        UpdateRing();
    }

    /// <summary>
    /// Update the size of the ring in a certain speed and update the edge colliders' segments and radius
    /// </summary>
    private void UpdateRing()
    {
        radius += scale_speed * Time.deltaTime * (default_collider_radius / defaultInnerRadius);
        if (numSegments < maxSegments)
        {
            numSegments += 1;
        }
        UpdateRingScale(RingMaterial.GetFloat("_OuterRadius") + scale_speed * Time.deltaTime);
    }

    private void UpdateRingScale(float amount)
    {
        RingMaterial.SetFloat("_OuterRadius", amount);
        RingMaterial.SetFloat("_InnerRadius", amount-RingGap);
        UpdateBounds();
        DrawCircle();
    }
    private void UpdateBounds()
    {
        outerCollider.radius = RingMaterial.GetFloat("_OuterRadius");
    }

    private float timer = 0f;
    /// <summary>
    /// only for test,not necessary to use coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shrink()
    {
        while(timer < 5f)
        {
            timer+= Time.deltaTime;
            UpdateRingScale(outerCollider.radius - scale_speed * Time.deltaTime);
            radius-=scale_speed* Time.deltaTime * (default_collider_radius/defaultInnerRadius);
            yield return null;
        }
    }


    private Vector2 onePoint = Vector2.zero;
    /// <summary>
    /// Draw edge colliders to fit the inner circle so they can trap the player inside
    /// </summary>
    private void DrawCircle()
    {
        Vector2[] points = new Vector2[numSegments + 1];
        float angleIncrement = 2f * Mathf.PI / numSegments;
        float angle = 0f;

        for (int i = 0; i <= numSegments; i++)
        {
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            Vector2 point = new Vector2(x, y) * radius;
            points[i] = point;

            angle += angleIncrement;
        }

        edgeCollider.points = points;
        bound.transform.position = points[0];
        onePoint = edgeCollider.points[0];
        onePoint.x = onePoint.x - default_collider_radius;
        //print("one point:" + onePoint);
    }

    /// <summary>
    /// Change the scaling speed via adding extra speed,positive extra_speed stands for accelerating the scale
    /// process and negative for the opposite
    /// </summary>
    /// <param name="extra_speed"></param>
    public void AddExtraScaleSpeed(float extra_speed)
    {
        scale_speed = extra_speed;
    }

    public void SetJointAnchor(Vector2 pos)
    {
        joint.anchor = pos;
    }

    public void SetJointRigidbody(Rigidbody2D rb)
    {
        joint.connectedBody = rb;
    }

    public float GetRadius()
    {
        return original_radius * RingMaterial.GetFloat("_OuterRadius") * 2;
    }

 
}
