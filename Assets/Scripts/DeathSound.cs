using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound;

    private void Awake()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
