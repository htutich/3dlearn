using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _speed = 10f;
    private float _lifeTime = 1f;
    private int _damageToGive;

    private void Start()
    {
        _damageToGive = Random.Range(20, 30);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "World")
        {
            Destroy(gameObject);
        }
    }
}
