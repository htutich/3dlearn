using UnityEngine;


public class EnemyTurretBulletController : MonoBehaviour
{
    #region Fields

    private float _speed = 10f;
    private float _lifeTime = 1f;
    private int _minDamage = 10;
    private int _maxDamage = 40;
    private int _damageToGive;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _damageToGive = Random.Range(_minDamage, _maxDamage);
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        var PlayerHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();
        PlayerHealthManager?.HurtPlayer(_damageToGive);
        Destroy(gameObject);
    }

    #endregion
}
