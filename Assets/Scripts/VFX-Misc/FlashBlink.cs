using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damagableObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;

    private float _blinkTimer;
    private bool _isBlicking;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
        _isBlicking = true;
    }

    private void Start()
    {
        if (damagableObject is Player player)
            player.OnFlashBlink += DamagebleObject_OnFlashBlink;
        if (damagableObject is AnimalAI animal)
            animal.OnAnimalFlashBlink += DamagebleObject_OnFlashBlink;
    }

    private void Update()
    {
        if (_isBlicking)
        {
            _blinkTimer -= Time.deltaTime;
            if (_blinkTimer < 0)
                SetDefaultMaterial();
        }
    }
    public void StopBlinking()
    {
        SetDefaultMaterial();
        _isBlicking = false;
    }

    private void DamagebleObject_OnFlashBlink(object sender, System.EventArgs e)
    {
        if (!_isBlicking) return;
        SetBlinkingMaterial();
    }

    private void SetBlinkingMaterial()
    {
        _blinkTimer = blinkDuration;
        _spriteRenderer.material = blinkMaterial;
    }
    private void SetDefaultMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
    }

    private void OnDestroy()
    {
        if (damagableObject is Player player)
            player.OnFlashBlink -= DamagebleObject_OnFlashBlink;
        if (damagableObject is AnimalAI animal)
            animal.OnAnimalFlashBlink -= DamagebleObject_OnFlashBlink;
    }
}
