using UnityEngine;
public class NextLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private LevelChanger levelChanger;
    public Vector3 position; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            levelChanger.FadeToLevel(this);
    }

    public string GetSceneName()
    {
        return sceneName;
    }
}
