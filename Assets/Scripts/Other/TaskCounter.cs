using UnityEngine;

public class TaskCounter : MonoBehaviour
{
    [SerializeField] private TaskManagement taskManagement;
    private CircleCollider2D circleCollider;

    private bool isFound;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && circleCollider != null && circleCollider.IsTouching(other) && !isFound)
        {
            isFound = true;
            if (taskManagement != null)
                taskManagement.SisterIsFound();
        }
    }

}
