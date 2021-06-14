using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneDirector : MonoBehaviour
{
    private PlayableDirector director;
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void StartTimeLine()
    {
        director.Play();
    }
}
