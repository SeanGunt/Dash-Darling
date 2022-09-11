using UnityEngine;
using System;

public class PlatformDestroyer : MonoBehaviour
{
    [NonSerialized] GameObject platformDestructionPoint;
    void Start()
    {
        platformDestructionPoint = GameObject.Find("Platform Destruction Point");
    }

    void Update()
    {
        if(transform.position.x < platformDestructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
