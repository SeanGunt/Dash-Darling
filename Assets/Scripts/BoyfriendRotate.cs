using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyfriendRotate : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    void Update()
    {
        float rotateZ = Mathf.SmoothStep(-6f, 6f, Mathf.PingPong(Time.time * speed, 1));
        this.transform.rotation = Quaternion.Euler(0,0,rotateZ);
    }
}
