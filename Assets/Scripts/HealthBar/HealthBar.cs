using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private MonoBehaviour healthSource;
    [SerializeField] private Gradient gradient;

    private IHealthSource _source;
    private Camera _camera;

    private void Awake()
    {
        _source = healthSource as IHealthSource;

        if (_source != null)
            _source.HealthChanged += OnHealthChanged;

        _camera = Camera.main;
    }

    private void OnHealthChanged(float valueAsPercantage)
    {
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
        if (_source != null)
            _source.HealthChanged -= OnHealthChanged;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (_source != null)
            _source.HealthChanged -= OnHealthChanged;
    }

}
