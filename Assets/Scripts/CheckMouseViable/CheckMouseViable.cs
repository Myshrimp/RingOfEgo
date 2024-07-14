using MY;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckMouseViable : MonoBehaviour
{
    public static CheckMouseViable instance;
    [SerializeField] Transform bound;
    [SerializeField] Transform center;
    public bool MouseIn = true;
    private Vector2 mouseWorldPos;

    [SerializeField] int numSegments = 32;
    [SerializeField] float radius = 10f;
    [SerializeField] EdgeCollider2D edgeCollider;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        DrawCircle();
    }
    private void Update()
    {
        mouseWorldPos = MouseInputHandler.ScreenPointToWorldPoint();
        if ((Ring.instance.transform.position - this.transform.position).magnitude >= GetRadius() - 0.5f)
        {
            MouseIn = false;
        }
        else
        {
            MouseIn = true;
        }
    }
    public float GetRadius()
    {
        return (bound.position-center.position).magnitude;
    }
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
    }
}
