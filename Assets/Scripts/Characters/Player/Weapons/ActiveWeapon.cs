using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Sword sword;
    public static ActiveWeapon Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Player.Instance.IsAlive())
           FollowMousePosition();
    }

    public Sword GetActiveWeapon()
    {
        return sword;
    }

    private void FollowMousePosition()
    {
        Vector3 mousePos = PlayerInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        transform.rotation = Quaternion.Euler(0, mousePos.x < playerPosition.x ? 180 : 0, 0);
    }
}
