using UnityEngine;

public class OnHoverSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mouseOverSound;
    public void OnHover()
    {
        audioSource.PlayOneShot(mouseOverSound);
    }
}
