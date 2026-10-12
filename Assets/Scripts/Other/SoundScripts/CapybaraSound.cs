using UnityEngine;

public class CapybaraSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; 

    public void Play()
    {
        if (audioSource == null) return;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
