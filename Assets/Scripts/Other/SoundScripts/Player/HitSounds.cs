using UnityEngine;

public class HitSounds : MonoBehaviour
{
    [SerializeField] private AudioSource hitDown;
    [SerializeField] private AudioSource hitUp;

    public void Play_HitUpper()
    {
        if (hitUp == null) return;
        hitUp.Play();
    }

    public void Play_HitBottom()
    {
        if (hitDown == null) return;
        hitDown.Play();
    }
}
