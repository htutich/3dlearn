using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _firstAidKit;
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

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_firstAidKit, new Vector3(transform.position.x, 0.0f, transform.position.z), transform.rotation);
            EventManager.TriggerEvent("EnemyDie");
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
