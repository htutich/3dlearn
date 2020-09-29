using UnityEngine;
using UnityEngine.UI;


public class EnemyTurretHealthManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _canvas;
    [SerializeField] private Slider _slider;
    private AudioSource _audioSource;

    private float _health = 100.0f;
    private float _currentHealth;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _canvas.SetActive(true);
        _audioSource = GetComponent<AudioSource>();

        _currentHealth = _health;
        _slider.value = CalculateHealth();
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion


    #region Methods

    public void HurtEnemy(int damage)
    {
        _audioSource.Play();

        _currentHealth -= damage;
        _slider.value = CalculateHealth();
    }

    private float CalculateHealth()
    {
        return _currentHealth / _health;
    }

    #endregion
}
