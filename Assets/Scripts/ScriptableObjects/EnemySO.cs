using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public int enemyDamageAmount;
}
