using UnityEngine;
using UnityEngine.UI;

public class HealthBarCapybara : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private AnimalEntity animalEntity;
    [SerializeField] private Gradient gradient;

    private Camera _camera;

    private void Awake()
    {
        if (animalEntity != null)
            animalEntity.HealthChanged += OnHealthChanged;

        _camera = Camera.main;
    }

    private void OnHealthChanged(float valueAsPercantage)
    {
        Debug.Log(valueAsPercantage);
        healthBarFilling.fillAmount = valueAsPercantage;
        healthBarFilling.color = gradient.Evaluate(valueAsPercantage);
    }

    private void LateUpdate()
    {
        if (_camera == null) return;

        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }

    public void HealthBarOnDeath()
    {
        if (animalEntity != null)
            animalEntity.HealthChanged -= OnHealthChanged;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (animalEntity != null)
            animalEntity.HealthChanged -= OnHealthChanged;
    }

}