using UnityEngine;

[CreateAssetMenu(fileName = "HealthValue", menuName = "ScriptableObjects/HealthValue", order = 1)]
public class HealthValue : ScriptableObject
{
    public int currentHealth;
    public float percentageHealth;
}
