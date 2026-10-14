using UnityEngine;

public class HitSounds : Sound
{
    [SerializeField] private AudioSource audioSource2;

    public void Play_bottom()
    {
        if (audioSource2 == null) return;
        audioSource2.Play();
    }
}
