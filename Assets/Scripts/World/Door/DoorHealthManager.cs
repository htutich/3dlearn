using UnityEngine;
using UnityEngine.UI;


public class DoorHealthManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _canvas;
    [SerializeField] private Slider _slider;

    private float _health = 100.0f;
    private float _currentHealth;
    private AudioSource _audioSource;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _canvas.SetActive(true);
        _audioSource = GetComponent<AudioSource>();

        _currentHealth = _health;
        _slider.value = CalculateHealth();
    }

    #endregion


    #region Methods

    public void HurtEnemy(float damage)
    {
        _audioSource.Play();
        _currentHealth -= damage;
        _slider.value = CalculateHealth();

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private float CalculateHealth()
    {
        return _currentHealth / _health;
    }

    #endregion
}
