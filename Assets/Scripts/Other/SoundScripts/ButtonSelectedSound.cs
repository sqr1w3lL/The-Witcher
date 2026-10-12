using UnityEngine;

public class ButtonSelectedSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}