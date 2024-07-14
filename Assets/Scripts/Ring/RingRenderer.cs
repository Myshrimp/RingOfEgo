using Unity.VisualScripting;
using UnityEngine;

public class RingRenderer : MonoBehaviour
{
    public Material mat;
    public Transform go;
    public float radius = 6;            //外半径 
    public float innerRadius = 3;       //内半径
    public int segments = 60;           //分割数 
    public int angleDegree = 360;       //扇形或扇面的角度

    private void Start()
    {
        transform.eulerAngles = Vector3.left * 90;
    }

    private void Update()
    {
        DrawRing(radius, innerRadius, segments, angleDegree, go.position);
    }

    /// <summary>
    /// 画圆环
    /// </summary>
    /// <param name="radius">圆半径</param>
    /// <param name="innerRadius">内圆半径</param>
    /// <param name="segments">圆的分割数</param>
    /// <param name="angleDegree">圆覆盖的角度</param>
    /// <param name="centerCircle">圆心坐标</param>
    void DrawRing(float radius, float innerRadius, int segments, int angleDegree, Vector3 centerCircle)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;
        //顶点
        Vector3[] vertices = new Vector3[segments * 2];
        angleDegree = Mathf.Clamp(angleDegree, 0, 360);
        float deltaAngle = Mathf.Deg2Rad * angleDegree / segments;
        float currentAngle = 0;
        for (int i = 0; i < vertices.Length; i += 2)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * innerRadius + centerCircle.x, sinA * innerRadius + centerCircle.y, 0);
            vertices[i + 1] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
            currentAngle += deltaAngle;
        }
        //三角形
        int[] triangles = new int[segments * 6];
        for (int i = 0, j = 0; i < segments * 6; i += 6, j += 2)
        {
            triangles[i] = j;
            triangles[i + 1] = (j + 1) % vertices.Length;
            triangles[i + 2] = (j + 3) % vertices.Length;
            triangles[i + 3] = j;
            triangles[i + 4] = (j + 3) % vertices.Length;
            triangles[i + 5] = (j + 2) % vertices.Length;
        }
        //uv:
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
        }
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        this.AddComponent<MeshCollider>();
    }
}
