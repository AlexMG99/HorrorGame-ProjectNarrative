using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;

    GameObject virtualCamera;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        virtualCamera = GameObject.Find("PlayerCamera");
    }

    public GameObject GetVirtualCamera()
    {
        return virtualCamera;
    }

    public void ChangeCameraOffset(Vector3 offset)
    {
        CinemachineTransposer cinTrans = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        cinTrans.m_FollowOffset = offset;
    }


    public void ChangeLevel(string newLevel)
    {
        SceneManager.LoadScene(newLevel);
    }
}
