using Unity.VisualScripting;
using UnityEngine;

public class RingRenderer : MonoBehaviour
{
    public Material mat;
    public Transform go;
    public float radius = 6;            //��뾶 
    public float innerRadius = 3;       //�ڰ뾶
    public int segments = 60;           //�ָ��� 
    public int angleDegree = 360;       //���λ�����ĽǶ�

    private void Start()
    {
        transform.eulerAngles = Vector3.left * 90;
    }

    private void Update()
    {
        DrawRing(radius, innerRadius, segments, angleDegree, go.position);
    }

    /// <summary>
    /// ��Բ��
    /// </summary>
    /// <param name="radius">Բ�뾶</param>
    /// <param name="innerRadius">��Բ�뾶</param>
    /// <param name="segments">Բ�ķָ���</param>
    /// <param name="angleDegree">Բ���ǵĽǶ�</param>
    /// <param name="centerCircle">Բ������</param>
    void DrawRing(float radius, float innerRadius, int segments, int angleDegree, Vector3 centerCircle)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;
        //����
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
        //������
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
