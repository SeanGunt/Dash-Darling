using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLook : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private float lookSpeed;
    [SerializeField] private GameObject reticle;

    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        GunLook();
        Reticle();
    }
    private void GunLook()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, -60, 85), Vector3.forward);
        turret.rotation = Quaternion.Slerp(turret.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    private void Reticle()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        reticle.transform.position = mouseCursorPos;
    }
}
