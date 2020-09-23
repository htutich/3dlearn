using UnityEngine;


public class BulletController : MonoBehaviour
{
    #region PrivateData

    private float _speed = 10f;
    private float _lifeTime = 1f;
    private int _damageToGive;

    #endregion


    #region Start

    private void Start()
    {
        _damageToGive = Random.Range(20, 30);
        Destroy(gameObject, _lifeTime);
    }

    #endregion


    #region Update

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    #endregion


    #region OnCollisionEnter

    private void OnCollisionEnter(Collision other)
    {
        var EnemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
        EnemyHealthManager?.HurtEnemy(_damageToGive);
        Destroy(gameObject);
    }

    #endregion
}
