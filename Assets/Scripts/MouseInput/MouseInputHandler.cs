using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MY
{
    public class MouseInputHandler : MonoBehaviour
    {
        public static Vector2 ScreenPointToWorldPoint()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.transform.position.z > 0 ? Camera.main.transform.position.z : -Camera.main.transform.position.z));//��Ļ����ת����������
            return worldPos;
        }

        public static GameObject MouseSelectedGameObject()
        {
            //��Ҫ��ײ������ſ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            return hit.collider.gameObject;
        }

        public static Vector2 MousePositionScreen()
        {
            return Input.mousePosition;
        }

    }
}

