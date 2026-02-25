using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Slider healthBar;
    public Image damageOverlay;              // full screen red/bloody image
    public Volume globalVolume;             // URP post processing volume

    [Header("Damage Overlay Settings")]
    public float overlayFadeSpeed = 2f;

    [Header("Vignette / Blur Thresholds")]
    public float criticalHealthThreshold = 30f;

    private SpawnManager _spawnManager;
    private Vignette _vignette;
    private DepthOfField _dof;
    private float _targetOverlayAlpha = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        _spawnManager = FindObjectOfType<SpawnManager>();

        if (globalVolume != null)
        {
            globalVolume.profile.TryGet(out _vignette);
            globalVolume.profile.TryGet(out _dof);
        }

        if (damageOverlay != null)
        {
            Color c = damageOverlay.color;
            c.a = 0f;
            damageOverlay.color = c;
        }

        UpdateHealthBar();
    }

    void Update()
    {
        // Fade overlay
        if (damageOverlay != null)
        {
            Color c = damageOverlay.color;
            c.a = Mathf.MoveTowards(c.a, _targetOverlayAlpha, overlayFadeSpeed * Time.deltaTime);
            damageOverlay.color = c;

            // slowly fade out after hit
            if (_targetOverlayAlpha > 0)
                _targetOverlayAlpha = Mathf.MoveTowards(_targetOverlayAlpha, 0f, overlayFadeSpeed * 0.3f * Time.deltaTime);
        }

        // Vignette and blur based on health
        UpdatePostProcessing();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // flash damage overlay
        _targetOverlayAlpha = 0.6f;

        UpdateHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        Debug.Log("Player died! Respawning...");
        currentHealth = maxHealth;
        UpdateHealthBar();
        _spawnManager.Respawn(gameObject);

        // reset effects
        _targetOverlayAlpha = 0f;
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;
    }

    void UpdatePostProcessing()
    {
        if (_vignette != null)
        {
            float intensity = currentHealth <= criticalHealthThreshold
                ? Mathf.Lerp(0.5f, 0.8f, 1f - (currentHealth / criticalHealthThreshold))
                : Mathf.Lerp(0f, 0.3f, 1f - (currentHealth / maxHealth));

            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, intensity, Time.deltaTime * 2f);
        }

        if (_dof != null)
        {
            float focusDist = currentHealth <= criticalHealthThreshold ? 0.5f : 10f;
            _dof.focusDistance.value = Mathf.Lerp(_dof.focusDistance.value, focusDist, Time.deltaTime * 2f);
        }
    }
}

