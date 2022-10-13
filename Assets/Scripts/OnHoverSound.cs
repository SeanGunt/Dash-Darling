using UnityEngine;

public class OnHoverSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mouseOverSound;
    public void OnHover()
    {
        audioSource.volume = 0.15f;
        audioSource.PlayOneShot(mouseOverSound);
    }
}
