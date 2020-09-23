using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthManager : MonoBehaviour
{
    #region PrivateData

    private float _health = 100.0f;
    private float _curHealth;
    public Slider slider;
    private AudioSource _audioSource;

    #endregion


    #region Events

    public delegate void EnemyAction();
    public static event EnemyAction onEnemyDie;

    #endregion


    #region Start

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _curHealth = _health;
        slider.value = CalculateHealth();
    }

    #endregion


    #region Update

    private void Update()
    {
        if (_curHealth <= 0)
        {
            Destroy(gameObject);
            onEnemyDie();
        }
    }

    #endregion


    #region Methods

    public void HurtEnemy(int damage)
    {
        _audioSource.Play();

        _curHealth -= damage;
        slider.value = CalculateHealth();
    }

    private float CalculateHealth()
    {
        return _curHealth / _health;
    }

    #endregion
}
