using UnityEngine;

public class SkeletonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource skeletonRoaming;
    [SerializeField] private AudioSource skeletonDeath;

    public void Play_Death()
    {
        if (skeletonDeath == null) return;
        skeletonDeath.Play();
    }

    public void Play_Roaming()
    {
        if (skeletonRoaming == null) return;
        skeletonRoaming.Play();
    }

    public void Stop_Roaming()
    {
        if (skeletonRoaming == null) return;
        skeletonRoaming.Stop();
    }
}
