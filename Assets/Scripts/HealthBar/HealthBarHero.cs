using UnityEngine;
using UnityEngine.UI;

public class HealthBarHero : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Player player;
    [SerializeField] private HealthValue hpValue;
    public float currentHealthAsPercentage = 1;

    private void Awake()
    {
        player.HeroHealthChanged += OnHealthChangedHero;

        SetHealthBar(hpValue.percentageHealth);
        currentHealthAsPercentage = hpValue.percentageHealth;
    }

    private void SetHealthBar(float hpPercantage)
    {
        healthBarFilling.fillAmount = hpPercantage;
        healthBarFilling.color = gradient.Evaluate(hpPercantage);
    }

    private void OnHealthChangedHero(float valueAsPercantage)
    {
        healthBarFilling.fillAmount = valueAsPercantage;
        healthBarFilling.color = gradient.Evaluate(valueAsPercantage);
        currentHealthAsPercentage = valueAsPercantage;
    }

}
