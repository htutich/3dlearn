using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthManager : MonoBehaviour
{
    #region Fields

    private float _health = 100.0f;
    private float _currentHealth;
    private AudioSource _audioSource;
    [SerializeField] private Slider _slider;

    #endregion


    #region Events

    public delegate void EnemyAction();
    public static event EnemyAction onEnemyDie;

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
            onEnemyDie();
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
