using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;

    public void SetDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
