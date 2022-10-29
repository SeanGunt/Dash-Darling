using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    private float turretFireRate = 17.5f;
    private float timeTillNextAttack;
    [SerializeField] private GameObject projectile, gunPivot;
    [SerializeField] private Transform ejectionPoint;
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Time.time - timeTillNextAttack >= 1f/turretFireRate)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(projectile, ejectionPoint.position, gunPivot.transform.rotation);
                timeTillNextAttack = Time.time;
            }
        }
    }
}
