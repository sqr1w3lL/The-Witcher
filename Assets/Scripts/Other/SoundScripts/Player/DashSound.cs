using UnityEngine;

public class DashSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; 

    public void Play()
    {
        if (audioSource == null) return;
        audioSource.Play();
    }
}
