using UnityEngine;

public class SkeletonSounds : Sound
{
    [SerializeField] private AudioSource audioSource2;

    public void Play_2()
    {
        if (audioSource2 == null) return;
        audioSource2.Play();
    }

    public void Stop_2()
    {
        if (audioSource2 == null) return;
        audioSource2.Stop();
    }
}
