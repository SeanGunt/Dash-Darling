using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLook : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private float lookSpeed;


    private void Update()
    {
        GunLook();
    }
    private void GunLook()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, -60, 85), Vector3.forward);
        turret.rotation = Quaternion.Slerp(turret.rotation, rotation, lookSpeed * Time.deltaTime);
    }
}
