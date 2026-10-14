using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource; 

    public void Play()
    {
        if (audioSource == null) return;
        audioSource.Play();
    }
}
