using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlendermanMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject jumpScareImage;
    Image jumpScareImageUI;
    AudioSource jumpScareAudioSource;

    public float checkDistance = 100.0f;

    List<GameObject> teleportPoints;

    // Start is called before the first frame update
    void Start()
    {
        teleportPoints = new List<GameObject>();

        GameObject teleportationList = GameObject.Find("Slenderman Teleport");

        jumpScareImageUI = jumpScareImage.GetComponent<Image>();
        jumpScareAudioSource = jumpScareImage.GetComponent<AudioSource>();

        int childCount = teleportationList.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            teleportPoints.Add(teleportationList.transform.GetChild(i).gameObject);
        }

        // Start Coroutine
        StartCoroutine("CheckPlayerDistance");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
    }

    IEnumerator CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > checkDistance)
        {
            transform.position = GetNearestTeleportationPoint();
        }
        if (checkDistance < 10.0f)
        {
            StartCoroutine("JumpScare");
        }

        yield return new WaitForSeconds(5.0f);
        StartCoroutine("CheckPlayerDistance");
    }

    Vector3 GetNearestTeleportationPoint()
    {
        float distance = 999999.0f;
        Vector3 tPointPosition = Vector3.zero;

        foreach (GameObject tPoint in teleportPoints)
        {
            float newDistance = Vector3.Distance(transform.position, tPoint.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                tPointPosition = tPoint.transform.position;
            }
        }

        return tPointPosition;
    }

    IEnumerator JumpScare()
    {
        jumpScareImage.SetActive(true);
        jumpScareAudioSource.Play();
        Debug.LogError("Ha entrado");

        while (jumpScareAudioSource.isPlaying)
        {
            float randCol = Random.RandomRange(0.1f, 1.0f);
            jumpScareImageUI.color = new Color(randCol, randCol, randCol);

            yield return new WaitForSeconds(0.1f);
        }

        jumpScareImage.SetActive(false);
    }
}
