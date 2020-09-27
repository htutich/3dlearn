﻿using UnityEngine;


public class PlayerBulletController : MonoBehaviour
{
    #region Fields

    private float _speed = 20f;
    private float _lifeTime = 2f;
    private int _minDamage = 10;
    private int _maxDamage = 20;
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
        var EnemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
        EnemyHealthManager?.HurtEnemy(_damageToGive);

        var DoorHealthManager = other.gameObject.GetComponent<DoorHealthManager>();
        DoorHealthManager?.HurtEnemy(_damageToGive);

        Destroy(gameObject);
    }

    #endregion
}
