using UnityEngine;

[CreateAssetMenu]
public class TaskCheckerSO : ScriptableObject
{
    public int flowerCount;
    public int mushroomCount;
    public int bushDeathCount;
    public int skeletonCount;
    public int sisterIsFoundCount;
    public int capybaraCount;
    public bool questCompleted = false;
    public int tasksCompletedCount;
}
