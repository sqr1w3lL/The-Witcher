using UnityEngine;

public class BushDestroyed : MonoBehaviour
{
    private TaskManagement _taskManagement;

    private void Awake()
    {
        _taskManagement = FindAnyObjectByType<TaskManagement>();
    }

    public void BushDead()
    {
        _taskManagement.BushIsDie();
    }
}

