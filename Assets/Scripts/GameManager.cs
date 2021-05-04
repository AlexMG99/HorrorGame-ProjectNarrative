using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
