using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDown : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
