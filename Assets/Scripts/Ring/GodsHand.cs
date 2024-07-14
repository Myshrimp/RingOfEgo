using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Cinemachine;
public class GodsHand : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCameraBase player_cam;
    [SerializeField]
    CinemachineVirtualCameraBase gods_cam;

    public static GodsHand instance;
    private void Awake()
    {
        instance = this;
    }
    public void EnterGodsPerspective()
    {
        player_cam.VirtualCameraGameObject.SetActive(false);
        gods_cam.VirtualCameraGameObject.SetActive(true);
    }

    public void EnterPlayerPerspective()
    {
        player_cam.VirtualCameraGameObject.SetActive(true);
        gods_cam.VirtualCameraGameObject.SetActive(false);
    }

    public void ChangeOrthoSize(float vol)
    {
       player_cam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += vol;
    }

    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EnterGodsPerspective();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnterPlayerPerspective();
        }
    }
    */
}
