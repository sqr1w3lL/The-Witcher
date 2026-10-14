using UnityEngine;

public class TaskCounter : MonoBehaviour
{
    [SerializeField] private TaskManagement taskManagement;
    private CircleCollider2D _circleCollider;

    private bool isFound;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _circleCollider != null && _circleCollider.IsTouching(other) && !isFound)
        {
            isFound = true;
            if (taskManagement != null)
                taskManagement.SisterIsFound();
        }
    }

}
