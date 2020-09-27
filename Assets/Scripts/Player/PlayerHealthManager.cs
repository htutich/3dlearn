using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Slider _slider;

    private float _health = 1000.0f;
    private float _currentHealth;
    private AudioSource _audioSource;

    #endregion


    #region Properties

    public float Health
    {
        get
        {
            return _health;
        }
    }
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    #endregion


    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _currentHealth = _health;
        _slider.value = CalculateHealth();
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            EventManager.TriggerEvent("PlayerDie");
            Destroy(gameObject);
        }
    }

    #endregion


    #region Methods

    public void HurtPlayer(float damage)
    {
        _audioSource.Play();

        _currentHealth -= damage;
        _slider.value = CalculateHealth();
    }

    public void HealPlayer(float health)
    {
        _currentHealth += health;
        _slider.value = CalculateHealth();
    }

    private float CalculateHealth()
    {
        return _currentHealth / _health;
    }

    #endregion
}
