using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    private float turretFireRate = 8.5f;
    private float timeTillNextAttack;
    [SerializeField] private GameObject projectile, gunPivot, reticle;
    [SerializeField] private Transform ejectionPoint;
    private AudioSource audioSource;
    [SerializeField] private AudioClip turretShotSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Time.time - timeTillNextAttack >= 1f/turretFireRate && reticle.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                PlaySound(turretShotSound);
                Instantiate(projectile, ejectionPoint.position, gunPivot.transform.rotation);
                timeTillNextAttack = Time.time;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
