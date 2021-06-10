using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlendermanMovement : MonoBehaviour
{
    public GameObject player;

    List<GameObject> teleportPoints;

    // Start is called before the first frame update
    void Start()
    {
        teleportPoints = new List<GameObject>();

        GameObject teleportationList = GameObject.Find("Slenderman Teleport");

        int childCount = teleportationList.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            teleportPoints.Add(teleportationList.transform.GetChild(i).gameObject);
        }

        // Start Coroutine
        StartCoroutine("CheckPlayerDistance");
    }

    IEnumerator CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance > 100)
        {
            transform.position = GetNearestTeleportationPoint();
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
}
