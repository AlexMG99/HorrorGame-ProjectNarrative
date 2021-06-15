using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class TransitionScene : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public GameObject middlePoint;

    public float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, 100.0f))
        {
            if (hit.transform.gameObject.name == "ColliderTop")
            {
                middlePoint.transform.localScale = middlePoint.transform.localScale + new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
                middlePoint.GetComponent<Image>().color = new Color(0.54f, 0.27f, 0.07f, middlePoint.GetComponent<Image>().color.a);
                if (middlePoint.transform.localScale.x > 5.0f)
                {
                    SceneManager.LoadScene("Level_2.1");
                }
            }
            else if (hit.transform.gameObject.name == "ColliderBottom")
            {
                middlePoint.transform.localScale = middlePoint.transform.localScale + new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
                middlePoint.GetComponent<Image>().color = new Color(0.25f, 0.41f, 0.88f, middlePoint.GetComponent<Image>().color.a);
                if (middlePoint.transform.localScale.x > 5.0f)
                {
                    SceneManager.LoadScene("Level_2.2");
                }
            }
            else
            {
                middlePoint.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                middlePoint.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, middlePoint.GetComponent<Image>().color.a);
            }
        }
    }
}
