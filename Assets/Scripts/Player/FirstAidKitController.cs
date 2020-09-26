using UnityEngine;


public class FirstAidKitController : MonoBehaviour
{
    #region Fields

    private AudioSource _audioSource;
    private int _minHealth = 100;
    private int _maxHealth = 300;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var randomHealth = Random.Range(_minHealth, _maxHealth);
            var playerHealth = other.gameObject.GetComponent<PlayerHealthManager>();

            if (playerHealth.CurrentHealth < playerHealth.Health)
            {
                _audioSource.Play();
                playerHealth.HealPlayer(randomHealth);
                Destroy(gameObject, 0.1f);
            }

        }
    }

    #endregion

}
